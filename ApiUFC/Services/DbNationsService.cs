using ApiUFC.Data;
using ApiUFC.Interfaces;
using ApiUFC.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiUFC.Services
{
    public class DbNationsService : INationsService
    {
        private readonly UfcDbContext _context;

        public DbNationsService(UfcDbContext context)
        {
            _context = context;
        }

        public async Task<List<Nation>> GetNations() =>
            await _context.Nations.ToListAsync();

        public async Task<Nation?> GetNation(int id) =>
            await _context.Nations.FindAsync(id);

        public async Task CreateNation(Nation nation)
        {
            _context.Nations.Add(nation);
            await _context.SaveChangesAsync();
        }

        public async Task<Nation?> UpdateNation(int id, Nation nation)
        {
            var dbNation = await _context.Nations.FindAsync(id);
            if (dbNation == null) return null;

            dbNation.Name = nation.Name;
            dbNation.IsoCode = nation.IsoCode;

            await _context.SaveChangesAsync();
            return dbNation;
        }

        public async Task DeleteNation(int id)
        {
            var nation = await _context.Nations.FindAsync(id);
            if (nation != null)
            {
                _context.Nations.Remove(nation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
