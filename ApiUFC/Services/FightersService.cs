using ApiUFC.Interfaces;
using ApiUFC.Models;

namespace ApiUFC.Services
{
    public class FightersService : IFightersService
    {
        private static readonly List<Fighter> AllFighters = new()
        {
            new Fighter { Id = 1, FullName = "Conor McGregor",     Nickname = "The Notorious", DivisionId = 2, NationId = 3, Wins = 22, Losses = 6, Draws = 0 },
            new Fighter { Id = 2, FullName = "Charles Oliveira",   Nickname = "Do Bronx",      DivisionId = 2, NationId = 2, Wins = 34, Losses = 9, Draws = 0 },
            new Fighter { Id = 3, FullName = "Kamaru Usman",       Nickname = "The Nigerian Nightmare", DivisionId = 3, NationId = 1, Wins = 20, Losses = 3, Draws = 0 },
            new Fighter { Id = 4, FullName = "Alexander Volkanovski", Nickname = "The Great",  DivisionId = 1, NationId = 1, Wins = 26, Losses = 4, Draws = 0 }
        };

        public Task<List<Fighter>> GetFighters()
        {
            return Task.FromResult(AllFighters);
        }

        public Task<Fighter?> GetFighter(int id)
        {
            var fighter = AllFighters.FirstOrDefault(f => f.Id == id);
            return Task.FromResult(fighter);
        }

        public Task CreateFighter(Fighter fighter)
        {
            if (AllFighters.Any())
                fighter.Id = AllFighters.Max(f => f.Id) + 1;
            else
                fighter.Id = 1;

            AllFighters.Add(fighter);
            return Task.CompletedTask;
        }

        public Task<Fighter?> UpdateFighter(int id, Fighter fighter)
        {
            var existing = AllFighters.FirstOrDefault(f => f.Id == id);
            if (existing != null)
            {
                existing.FullName = fighter.FullName;
                existing.Nickname = fighter.Nickname;
                existing.DivisionId = fighter.DivisionId;
                existing.NationId = fighter.NationId;
                existing.Wins = fighter.Wins;
                existing.Losses = fighter.Losses;
                existing.Draws = fighter.Draws;
            }
            return Task.FromResult(existing);
        }

        public Task DeleteFighter(int id)
        {
            var fighter = AllFighters.FirstOrDefault(f => f.Id == id);
            if (fighter != null)
                AllFighters.Remove(fighter);

            return Task.CompletedTask;
        }
    }
}
