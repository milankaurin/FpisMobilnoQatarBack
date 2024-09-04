using Domain.Model;
using MobilnoQatarBack.Data;
using MobilnoQatarBack.DTO;
using MobilnoQatarBack.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MobilnoQatarBack.Services
{
    public class UtakmicaService : IUtakmicaService
    {
        private readonly DataContext _context;

        public UtakmicaService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UtakmicaDTO>> GetAllUtakmiceAsync()
        {
            var utakmice = await _context.Utakmice
                .Include(u => u.Tim1)
                .Include(u => u.Tim2)
                .Include(u => u.Stadion)
                .ToListAsync();
            return utakmice.Adapt<List<UtakmicaDTO>>();
        }

        public async Task<UtakmicaDTO> GetUtakmicaByIdAsync(int id)
        {
            var utakmica = await _context.Utakmice
                .Include(u => u.Tim1)
                .Include(u => u.Tim2)
                .Include(u => u.Stadion)
                .FirstOrDefaultAsync(u => u.Id == id);
            return utakmica?.Adapt<UtakmicaDTO>();
        }

        public async Task<UtakmicaDTO> CreateUtakmicaAsync(UtakmicaDTO utakmicaDTO)
        {
            if (utakmicaDTO.Tim1Id == utakmicaDTO.Tim2Id)
                throw new InvalidOperationException("A team cannot play against itself. Please select two different teams.");

            if (!await AreTeamsInSameGroup(utakmicaDTO.Tim1Id, utakmicaDTO.Tim2Id))
                throw new InvalidOperationException("Teams are not in the same group. Matches can only be scheduled between teams in the same group.");

            if (await HaveTeamsPlayedBefore(utakmicaDTO.Tim1Id, utakmicaDTO.Tim2Id))
                throw new InvalidOperationException("These teams have already played against each other in the group stage.");

            // Skip stadion availability and time checks if the match is forfeited
            if (!utakmicaDTO.Tim1Predao && !utakmicaDTO.Tim2Predao)
            {
                if (!IsValidTime(utakmicaDTO.VremePocetka))
                    throw new InvalidOperationException("Match start time is invalid. Matches must start between 14:00 and 23:00.");

                if (!await IsStadionAvailable(utakmicaDTO.StadionId, utakmicaDTO.VremePocetka))
                    throw new InvalidOperationException("The selected stadium is not available at the chosen time.");

                if (await IsTeamAlreadyPlaying(utakmicaDTO.Tim1Id, utakmicaDTO.VremePocetka) ||
                    await IsTeamAlreadyPlaying(utakmicaDTO.Tim2Id, utakmicaDTO.VremePocetka))
                    throw new InvalidOperationException("One of the teams is already scheduled for another match at the same time.");
            }

            var utakmica = utakmicaDTO.Adapt<Utakmica>();
            utakmica.Predato = utakmicaDTO.Tim1Predao || utakmicaDTO.Tim2Predao;
            if (utakmicaDTO.Tim1Predao)
            {
                utakmica.Tim1Golovi = 0;
                utakmica.Tim2Golovi = 3;
            }
            else if (utakmicaDTO.Tim2Predao)
            {
                utakmica.Tim1Golovi = 3;
                utakmica.Tim2Golovi = 0;
            }

            _context.Utakmice.Add(utakmica);
            await _context.SaveChangesAsync();

            if (utakmica.Predato)
            {
                // Ažuriraj statistiku timova
                await UpdateTeamStats(utakmica.Tim1Id, utakmica.Tim1Golovi, utakmica.Tim2Golovi);
                await UpdateTeamStats(utakmica.Tim2Id, utakmica.Tim2Golovi, utakmica.Tim1Golovi);
            }

            return utakmica.Adapt<UtakmicaDTO>();
        }

        private async Task<bool> IsTeamAlreadyPlaying(int teamId, DateTime matchTime)
        {
            // Postavljanje granica vremena za preklapanje utakmica
            DateTime startTime = matchTime.AddHours(-2);
            DateTime endTime = matchTime.AddHours(2);

            // Provera da li tim već ima zakazanu utakmicu u navedenom vremenskom opsegu
            return await _context.Utakmice.AnyAsync(u =>
                (u.Tim1Id == teamId || u.Tim2Id == teamId) &&
                u.VremePocetka.Date == matchTime.Date &&
                u.VremePocetka >= startTime &&
                u.VremePocetka <= endTime);
        }

        private async Task UpdateTeamStats(int timId, int? scoredGoals, int? concededGoals)
        {
            var tim = await _context.Timovi.FindAsync(timId);
            if (tim != null && scoredGoals.HasValue && concededGoals.HasValue)
            {
                tim.BrojDatihGolova += scoredGoals.Value;
                tim.BrojPrimljenihGolova += concededGoals.Value;

                if (scoredGoals > concededGoals)
                {
                    tim.BrojPoena += 3;
                    tim.BrojPobeda += 1;
                }
                else if (scoredGoals == concededGoals)
                {
                    tim.BrojPoena += 1;
                    tim.BrojNeresenih += 1;
                }
                else
                {
                    tim.BrojPoraza += 1;
                }
                await _context.SaveChangesAsync();
            }
        }

        private void UpdateTeamStats1(Tim tim, int? scoredGoals, int? concededGoals)
        {
            if (scoredGoals.HasValue && concededGoals.HasValue)
            {
                tim.BrojDatihGolova += scoredGoals.Value;
                tim.BrojPrimljenihGolova += concededGoals.Value;

                if (scoredGoals > concededGoals)
                {
                    tim.BrojPoena += 3;
                    tim.BrojPobeda += 1;
                }
                else if (scoredGoals == concededGoals)
                {
                    tim.BrojPoena += 1;
                    tim.BrojNeresenih += 1;
                }
                else
                {
                    tim.BrojPoraza += 1;
                }
            }
        }

        private async Task<bool> HaveTeamsPlayedBefore(int tim1Id, int tim2Id)
        {
            return await _context.Utakmice.AnyAsync(u =>
                (u.Tim1Id == tim1Id && u.Tim2Id == tim2Id) || (u.Tim1Id == tim2Id && u.Tim2Id == tim1Id));
        }

        public async Task<bool> UpdateUtakmicaAsync(int id, UtakmicaDTO utakmicaDTO)
        {
            var utakmica = await _context.Utakmice.FindAsync(id);
            if (utakmica == null) return false;
            utakmicaDTO.Adapt(utakmica);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUtakmicaAsync(int id)
        {
            var utakmica = await _context.Utakmice.FindAsync(id);
            if (utakmica == null) return false;
            _context.Utakmice.Remove(utakmica);
            await _context.SaveChangesAsync();
            return true;
        }

        //public async Task<bool> SetRezultatUtakmiceAsync(int utakmicaId, int tim1Golovi, int tim2Golovi)
        //{
        //    var utakmica = await _context.Utakmice.Include(u => u.Tim1).Include(u => u.Tim2).FirstOrDefaultAsync(u => u.Id == utakmicaId);
        //    if (utakmica == null) return false;

        //    // Postavljanje golova
        //    utakmica.Tim1Golovi = tim1Golovi;
        //    utakmica.Tim2Golovi = tim2Golovi;

        //    // Ažuriranje statistike timova
        //    if (utakmica.Tim1 != null && utakmica.Tim2 != null)
        //    {
        //        UpdateTeamStats(utakmica.Tim1Id, tim1Golovi, tim2Golovi);
        //        UpdateTeamStats(utakmica.Tim2Id, tim2Golovi, tim1Golovi);
        //    }

        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> SetRezultatUtakmiceAsync(int utakmicaId, int tim1Golovi, int tim2Golovi)
        {
            var utakmica = await _context.Utakmice.Include(u => u.Tim1).Include(u => u.Tim2).FirstOrDefaultAsync(u => u.Id == utakmicaId);
            if (utakmica == null)
            {
                return false;
            }

            // Provera da li je vreme početka utakmice manje od trenutnog vremena
            if (utakmica.VremePocetka > DateTime.Now)
            {
                // Logika za rukovanje slučajem kada se pokuša postaviti rezultat pre početka utakmice
                throw new InvalidOperationException("Nije moguće postaviti rezultat pre vremena početka utakmice.");
            }

            // Postavljanje golova
            utakmica.Tim1Golovi = tim1Golovi;
            utakmica.Tim2Golovi = tim2Golovi;

            // Ažuriranje statistike timova
            if (utakmica.Tim1 != null && utakmica.Tim2 != null)
            {
                UpdateTeamStats1(utakmica.Tim1, tim1Golovi, tim2Golovi);
                UpdateTeamStats1(utakmica.Tim2, tim2Golovi, tim1Golovi);
            }

            await _context.SaveChangesAsync(); // Sačuvaj sve promene odjednom
            return true;
        }


        private bool IsValidTime(DateTime time)
        {
            return time.TimeOfDay >= new TimeSpan(14, 0, 0) && time.TimeOfDay <= new TimeSpan(23, 0, 0);
        }

        private async Task<bool> IsStadionAvailable(int? stadionId, DateTime time)
        { if (stadionId == null) return false; 
            return !await _context.Utakmice.AnyAsync(u =>
        u.StadionId == stadionId &&
        u.VremePocetka.Date == time.Date &&
        u.VremePocetka >= time.AddHours(-4) &&
        u.VremePocetka <= time.AddHours(4));
        }

        private async Task<bool> AreTeamsInSameGroup(int tim1Id, int tim2Id)
        {
            var tim1 = await _context.Timovi.FindAsync(tim1Id);
            var tim2 = await _context.Timovi.FindAsync(tim2Id);
            return tim1 != null && tim2 != null && tim1.GrupaId == tim2.GrupaId;
        }
        //GetUtakmiceByGroupIdAsync(groupId): List<UtakmicaDTO>
        //CreateUtakmica(DTO): Task<>
        //SetRezultat(id,data): Task<>
        //DeleteUtakmica(id): Task<>
        public async Task<IEnumerable<UtakmicaDTO>> GetUtakmiceByGroupIdAsync(int groupId)
        {
            var utakmice = await _context.Utakmice.Include(u =>u.Tim1).Include(u => u.Tim2).Include(u => u.Stadion).Where(u => u.Tim1.GrupaId == groupId || u.Tim2.GrupaId == groupId).ToListAsync();

            return utakmice.Adapt<List<UtakmicaDTO>>();
        }


        public async Task<bool> DeleteAllUtakmiceAsync()
        {
            var allUtakmice = await _context.Utakmice.ToListAsync();
            if (!allUtakmice.Any()) return false;

            _context.Utakmice.RemoveRange(allUtakmice);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
