using Domain.Model;
using MobilnoQatarBack.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobilnoQatarBack.Interfaces
{
    public interface IStadionService
    {
        Task<IEnumerable<StadionDTO>> GetAllStadioniAsync();
        Task<StadionDTO> GetStadionByIdAsync(int id);
        Task<StadionDTO> CreateStadionAsync(StadionDTO stadionDTO);
        Task<bool> UpdateStadionAsync(int id, StadionDTO stadionDTO);
        Task<bool> DeleteStadionAsync(int id);
    }

}