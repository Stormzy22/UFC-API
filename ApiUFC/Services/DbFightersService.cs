using ApiUFC.Data;
using ApiUFC.Interfaces;
using ApiUFC.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiUFC.Services
{
    public class DbFightersService : IFightersService
    {
        private readonly UfcDbContext _context;

        public DbFightersService(UfcDbContext context)
        {
            _context = context;
        }

        public async Task<List<Fighter>> GetFighters()
        {
            return await _context.Fighters.ToListAsync();
        }

        public async Task<Fighter?> GetFighter(int id)
        {
            return await _context.Fighters.FindAsync(id);
        }

        public async Task CreateFighter(Fighter fighter)
        {
            _context.Fighters.Add(fighter);
            await _context.SaveChangesAsync();
        }

        public async Task<Fighter?> UpdateFighter(int id, Fighter fighter)
        {
            var dbFighter = await _context.Fighters.FindAsync(id);
            if (dbFighter == null)
                return null;

            dbFighter.FullName = fighter.FullName;
            dbFighter.Nickname = fighter.Nickname;
            dbFighter.DivisionId = fighter.DivisionId;
            dbFighter.NationId = fighter.NationId;
            dbFighter.Wins = fighter.Wins;
            dbFighter.Losses = fighter.Losses;
            dbFighter.Draws = fighter.Draws;

            await _context.SaveChangesAsync();
            return dbFighter;
        }

        public async Task DeleteFighter(int id)
        {
            var fighter = await _context.Fighters.FindAsync(id);
            if (fighter != null)
            {
                _context.Fighters.Remove(fighter);
                await _context.SaveChangesAsync();
            }
        }
    }
}
