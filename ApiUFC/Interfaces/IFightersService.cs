using ApiUFC.Models;

namespace ApiUFC.Interfaces
{
    public interface IFightersService
    {
        Task<List<Fighter>> GetFighters();
        Task<Fighter?> GetFighter(int id);
        Task CreateFighter(Fighter fighter);
        Task<Fighter?> UpdateFighter(int id, Fighter fighter);
        Task DeleteFighter(int id);
    }
}
