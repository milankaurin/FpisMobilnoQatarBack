using MobilnoQatarBack.DTO;

namespace MobilnoQatarBack.Interfaces
{
    public interface IUtakmicaService
    {
        Task<IEnumerable<UtakmicaDTO>> GetAllUtakmiceAsync();
        Task<UtakmicaDTO> GetUtakmicaByIdAsync(int id);
        Task<UtakmicaDTO> CreateUtakmicaAsync(UtakmicaDTO utakmicaDTO);
        Task<bool> UpdateUtakmicaAsync(int id, UtakmicaDTO utakmicaDTO);
        Task<bool> DeleteUtakmicaAsync(int id);
        Task<bool> SetRezultatUtakmiceAsync(int utakmicaId, int tim1Golovi, int tim2Golovi);
        Task<IEnumerable<UtakmicaDTO>> GetUtakmiceByGroupIdAsync(int groupId);
        Task<bool> DeleteAllUtakmiceAsync();

    }
}
