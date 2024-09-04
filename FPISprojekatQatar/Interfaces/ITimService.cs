using MobilnoQatarBack.DTO;

namespace MobilnoQatarBack.Interfaces
{
    public interface ITimService
    {
        Task<IEnumerable<TimDTO>> GetAllTimoviAsync();
        Task<TimDTO> GetTimByIdAsync(int id);
        Task<TimDTO> CreateTimAsync(TimDTO timDTO);
        Task<bool> UpdateTimAsync(int id, TimDTO timDTO);
        Task<bool> DeleteTimAsync(int id);
        Task<bool> AddTimToGrupaAsync(int timId, int grupaId);
        Task<bool> RemoveTimFromGrupaAsync(int timId);
        Task<IEnumerable<TimDTO>> GetTimByGrupaIdAsync(int grupaId);

        Task<bool> DeleteAllTimoviAsync();
    }
}
