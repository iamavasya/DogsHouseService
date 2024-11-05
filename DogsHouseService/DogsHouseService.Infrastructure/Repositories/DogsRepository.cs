using DogsHouseService.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogsHouseService.Infrastructure.Models.Entities;
using DogsHouseService.Infrastructure.DbContextes;
using Microsoft.EntityFrameworkCore;

namespace DogsHouseService.Infrastructure.Repositories
{
    public class DogsRepository : IDogsRepository
    {
        private readonly DogsDbContext _context;

        public DogsRepository(DogsDbContext context)
        {
            _context = context;
        }
        public async Task<IList<Dog>> GetDogsAsync()
        {
            return await _context.Dogs.ToListAsync();
        }
        public async Task<IList<Dog>> GetDogsAsync(int pageNumber, int pageSize)
        {
            return await _context.Dogs.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<Dog> GetByIdAsync(int id)
        {
            return await _context.Dogs.FindAsync(id);
        }
        public async Task<Dog?> CreateDogAsync(Dog dog)
        {
            _context.Dogs.Add(dog);
            await _context.SaveChangesAsync();
            return dog;
        }

        public async Task<Dog?> GetDogByNameAsync(string? name)
        {
            return await _context.Dogs.FirstOrDefaultAsync(dog => dog.Name == name);
        }
    }
}
