using Domain.Model;
using MobilnoQatarBack.Data;
using MobilnoQatarBack.DTO;
using MobilnoQatarBack.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MobilnoQatarBack.Services
{
    public class TimService : ITimService
    {
        private readonly DataContext _context;

        public TimService(DataContext context)
        {
            _context = context;
        }

        //GetTimById(id): TimDTO
        //CreateTim(DTO): Task<>
        //DeleteTim(id) : Task<>
        //GetTimByGrupaId(grupaId): Task<>
        //UpdateTim(id,data): Task<>

        public async Task<IEnumerable<TimDTO>> GetAllTimoviAsync()
        {
            var timovi = await _context.Timovi.ToListAsync();
            return timovi.Adapt<List<TimDTO>>();
        }

        public async Task<TimDTO> GetTimByIdAsync(int id)
        {
            var tim = await _context.Timovi.FindAsync(id);
            return tim?.Adapt<TimDTO>();
        }

        public async Task<TimDTO> CreateTimAsync(TimDTO timDTO)
        {
            bool teamExists = await _context.Timovi.AnyAsync(t => t.ImeTima == timDTO.ImeTima);
            if (teamExists)
            {
                throw new InvalidOperationException("Tim sa istim imenom već postoji u bazi.");
            }

            var tim = timDTO.Adapt<Tim>();

            // Provera da li je dodeljena grupa i validacija kapaciteta
            if (tim.GrupaId.HasValue)
            {
                var grupa = await _context.Grupe.Include(g => g.Timovi).FirstOrDefaultAsync(g => g.Id == tim.GrupaId.Value);
                if (grupa == null)
                {
                    throw new InvalidOperationException("Specifikovana grupa ne postoji.");
                }
                if (grupa.Timovi.Count >= 4)
                {
                    throw new InvalidOperationException("Grupa već ima maksimalan broj timova (4).");
                }
            }

            _context.Timovi.Add(tim);
            await _context.SaveChangesAsync();
            return tim.Adapt<TimDTO>();
        }

        public async Task<bool> UpdateTimAsync(int id, TimDTO timDTO)
        {
            var tim = await _context.Timovi.FindAsync(id);
            if (tim == null) return false;
            timDTO.Adapt(tim);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTimAsync(int id)
        {
            var tim = await _context.Timovi.FindAsync(id);
            if (tim == null) return false;
            _context.Timovi.Remove(tim);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddTimToGrupaAsync(int timId, int grupaId)
        {
            var tim = await _context.Timovi.FindAsync(timId);
            var grupa = await _context.Grupe.Include(g => g.Timovi).FirstOrDefaultAsync(g => g.Id == grupaId);
            if (tim == null || grupa == null || grupa.Timovi.Count >= 4) return false;
            tim.Grupa = grupa;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveTimFromGrupaAsync(int timId)
        {
            var tim = await _context.Timovi.FindAsync(timId);
            if (tim == null || tim.GrupaId == null) return false;
            tim.GrupaId = null;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TimDTO>> GetTimByGrupaIdAsync(int grupaId)
        {
                  return await _context.Timovi
            .Where(t => t.GrupaId == grupaId)
            .Select(t => new TimDTO
            {
                Id = t.Id,
                ImeTima = t.ImeTima,
                BrojDatihGolova=t.BrojDatihGolova,
                BrojNeresenih=t.BrojNeresenih,
                BrojPobeda=t.BrojPobeda,
                BrojPoena=t.BrojPoena,
                BrojPoraza=t.BrojPoraza,
                BrojPrimljenihGolova=t.BrojPrimljenihGolova,
                GrupaId = t.GrupaId,


            }).ToListAsync();
    }


        public async Task<bool> DeleteAllTimoviAsync()
        {
            var allTimovi = await _context.Timovi.ToListAsync();
            if (!allTimovi.Any()) return false;

            _context.Timovi.RemoveRange(allTimovi);
            await _context.SaveChangesAsync();
            return true;
        }

    }
    }

