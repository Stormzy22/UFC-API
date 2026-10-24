using ApiUFC.Models;

namespace ApiUFC.Interfaces
{
    public interface IDivisionsService
    {
        Task<List<Division>> GetDivisions();
        Task<Division?> GetDivision(int id);
        Task CreateDivision(Division division);
        Task<Division?> UpdateDivision(int id, Division division);
        Task DeleteDivision(int id);
    }
}
