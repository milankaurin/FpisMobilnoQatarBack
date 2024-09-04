using Domain.Model;
using MobilnoQatarBack.Data;
using MobilnoQatarBack.DTO;
using MobilnoQatarBack.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MobilnoQatarBack.Services
{
    public class GrupaService : IGrupaService
    {
        private readonly DataContext _context;

        public GrupaService(DataContext context)
        {
            _context = context;
        }

        //GetAllGrupe():List<GrupaDTO>
        //CreateGrupa(DTO):Task<>


        public async Task<IEnumerable<GrupaDTO>> GetAllGrupeAsync()
        {
            var grupe = await _context.Grupe.ToListAsync();
            return grupe.Adapt<List<GrupaDTO>>();
        }

        public async Task<GrupaDTO> GetGrupaByIdAsync(int id)
        {
            var grupa = await _context.Grupe.FindAsync(id);
            return grupa?.Adapt<GrupaDTO>();
        }

        public async Task<GrupaDTO> CreateGrupaAsync(GrupaDTO grupaDTO)
        {
            var grupa = grupaDTO.Adapt<Grupa>();
            _context.Grupe.Add(grupa);
            await _context.SaveChangesAsync();
            return grupa.Adapt<GrupaDTO>();
        }

        public async Task<bool> UpdateGrupaAsync(int id, GrupaDTO grupaDTO)
        {
            var grupa = await _context.Grupe.FindAsync(id);
            if (grupa == null) return false;
            grupaDTO.Adapt(grupa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteGrupaAsync(int id)
        {
            var grupa = await _context.Grupe.FindAsync(id);
            if (grupa == null) return false;
            _context.Grupe.Remove(grupa);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> AddTimToGrupaAsync(int grupaId, int timId)
        {
            var grupa = await _context.Grupe.Include(g => g.Timovi).FirstOrDefaultAsync(g => g.Id == grupaId);
            var tim = await _context.Timovi.FindAsync(timId);
            if (grupa == null || tim == null || grupa.Timovi.Count >= 4) return false;

            grupa.Timovi.Add(tim);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveTimFromGrupaAsync(int timId)
        {
            var tim = await _context.Timovi.FindAsync(timId);
            if (tim == null || tim.GrupaId == null) return false;

            tim.Grupa = null;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAllDataAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Brisanje svih utakmica
                var utakmice = await _context.Utakmice.ToListAsync();
                _context.Utakmice.RemoveRange(utakmice);

                // Brisanje svih timova
                var timovi = await _context.Timovi.ToListAsync();
                _context.Timovi.RemoveRange(timovi);

                // Brisanje svih grupa
                var grupe = await _context.Grupe.ToListAsync();
                _context.Grupe.RemoveRange(grupe);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log exception here
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}

