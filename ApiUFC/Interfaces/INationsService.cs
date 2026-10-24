using ApiUFC.Models;

namespace ApiUFC.Interfaces
{
    public interface INationsService
    {
        Task<List<Nation>> GetNations();
        Task<Nation?> GetNation(int id);
        Task CreateNation(Nation nation);
        Task<Nation?> UpdateNation(int id, Nation nation);
        Task DeleteNation(int id);
    }
}
