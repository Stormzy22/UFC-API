using ApiUFC.Interfaces;
using ApiUFC.Models;

namespace ApiUFC.Services
{
    public class NationsService : INationsService
    {
        private static readonly List<Nation> AllNations = new()
        {
            new Nation { Id = 1, Name = "United States", IsoCode = "US" },
            new Nation { Id = 2, Name = "Brazil", IsoCode = "BR" },
            new Nation { Id = 3, Name = "United Kingdom", IsoCode = "GB" },
            new Nation { Id = 4, Name = "Russia", IsoCode = "RU" }
        };

        public Task<List<Nation>> GetNations()
        {
            return Task.FromResult(AllNations);
        }

        public Task<Nation?> GetNation(int id)
        {
            var nation = AllNations.FirstOrDefault(n => n.Id == id);
            return Task.FromResult(nation);
        }

        // Extra CRUD (nu nog niet nodig voor de opdracht, maar zelfde stijl):
        public Task CreateNation(Nation nation)
        {
            if (AllNations.Any())
                nation.Id = AllNations.Max(n => n.Id) + 1;
            else
                nation.Id = 1;

            AllNations.Add(nation);
            return Task.CompletedTask;
        }

        public Task<Nation?> UpdateNation(int id, Nation nation)
        {
            var existing = AllNations.FirstOrDefault(n => n.Id == id);
            if (existing != null)
            {
                existing.Name = nation.Name;
                existing.IsoCode = nation.IsoCode;
            }
            return Task.FromResult(existing);
        }

        public Task DeleteNation(int id)
        {
            var nation = AllNations.FirstOrDefault(n => n.Id == id);
            if (nation != null)
                AllNations.Remove(nation);

            return Task.CompletedTask;
        }
    }
}
