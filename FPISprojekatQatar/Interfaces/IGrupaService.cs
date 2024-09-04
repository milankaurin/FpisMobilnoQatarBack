using MobilnoQatarBack.DTO;

namespace MobilnoQatarBack.Interfaces
{
    public interface IGrupaService
    {
        Task<IEnumerable<GrupaDTO>> GetAllGrupeAsync();
        Task<GrupaDTO> GetGrupaByIdAsync(int id);
        Task<GrupaDTO> CreateGrupaAsync(GrupaDTO grupaDTO);
        Task<bool> UpdateGrupaAsync(int id, GrupaDTO grupaDTO);
        Task<bool> AddTimToGrupaAsync(int grupaId, int timId);
        Task<bool> RemoveTimFromGrupaAsync(int timId);
        Task<bool> DeleteGrupaAsync(int id);
        Task<bool> DeleteAllDataAsync();
    }
}
