using ApiUFC.Data;
using ApiUFC.Interfaces;
using ApiUFC.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiUFC.Services
{
    public class DbDivisionsService : IDivisionsService
    {
        private readonly UfcDbContext _context;

        public DbDivisionsService(UfcDbContext context)
        {
            _context = context;
        }

        public async Task<List<Division>> GetDivisions() =>
            await _context.Divisions.ToListAsync();

        public async Task<Division?> GetDivision(int id) =>
            await _context.Divisions.FindAsync(id);

        public async Task CreateDivision(Division division)
        {
            _context.Divisions.Add(division);
            await _context.SaveChangesAsync();
        }

        public async Task<Division?> UpdateDivision(int id, Division division)
        {
            var dbDivision = await _context.Divisions.FindAsync(id);
            if (dbDivision == null) return null;

            dbDivision.Name = division.Name;
            dbDivision.Gender = division.Gender;

            await _context.SaveChangesAsync();
            return dbDivision;
        }

        public async Task DeleteDivision(int id)
        {
            var division = await _context.Divisions.FindAsync(id);
            if (division != null)
            {
                _context.Divisions.Remove(division);
                await _context.SaveChangesAsync();
            }
        }
    }
}
