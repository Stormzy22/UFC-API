using ApiUFC.Interfaces;
using ApiUFC.Models;
using System;
namespace ApiUFC.Services
{
    public class DivisionsService : IDivisionsService
    {
        private static readonly List<Division> AllDivisions = new()
        {
            new Division { Id = 1, Name = "Featherweight",  Gender = "Male" },
            new Division { Id = 2, Name = "Lightweight",    Gender = "Male" },
            new Division { Id = 3, Name = "Welterweight",   Gender = "Male" },
            new Division { Id = 4, Name = "Women Strawweight", Gender = "Female" }
        };

        public Task<List<Division>> GetDivisions()
        {
            return Task.FromResult(AllDivisions);
        }

        public Task<Division?> GetDivision(int id)
        {
            var division = AllDivisions.FirstOrDefault(d => d.Id == id);
            return Task.FromResult(division);
        }

        public Task CreateDivision(Division division)
        {
            if (AllDivisions.Any())
                division.Id = AllDivisions.Max(d => d.Id) + 1;
            else
                division.Id = 1;

            AllDivisions.Add(division);
            return Task.CompletedTask;
        }

        public Task<Division?> UpdateDivision(int id, Division division)
        {
            var existing = AllDivisions.FirstOrDefault(d => d.Id == id);
            if (existing != null)
            {
                existing.Name = division.Name;
                existing.Gender = division.Gender;
            }
            return Task.FromResult(existing);
        }

        public Task DeleteDivision(int id)
        {
            var division = AllDivisions.FirstOrDefault(d => d.Id == id);
            if (division != null)
                AllDivisions.Remove(division);

            return Task.CompletedTask;
        }
    }
}
