using Domain.Model;
using MobilnoQatarBack.Data;
using MobilnoQatarBack.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MobilnoQatarBack.DTO;
using Mapster;

namespace MobilnoQatarBack.Services
{
    public class StadionService : IStadionService
    {
        private readonly DataContext _context;

        public StadionService(DataContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<StadionDTO>> GetAllStadioniAsync()
        {
            var stadioni = await _context.Stadioni.ToListAsync();
            return stadioni.Adapt<List<StadionDTO>>();
        }

        public async Task<StadionDTO> GetStadionByIdAsync(int id)
        {
            var stadion = await _context.Stadioni.FindAsync(id);
            return stadion?.Adapt<StadionDTO>();
        }

        public async Task<StadionDTO> CreateStadionAsync(StadionDTO stadionDTO)
        {
            var stadion = stadionDTO.Adapt<Stadion>();
            _context.Stadioni.Add(stadion);
            await _context.SaveChangesAsync();
            return stadion.Adapt<StadionDTO>();
        }

        public async Task<bool> UpdateStadionAsync(int id, StadionDTO stadionDTO)
        {
            var stadion = await _context.Stadioni.FindAsync(id);
            if (stadion == null) return false;
            stadionDTO.Adapt(stadion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStadionAsync(int id)
        {
            var stadion = await _context.Stadioni.FindAsync(id);
            if (stadion == null) return false;
            _context.Stadioni.Remove(stadion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}