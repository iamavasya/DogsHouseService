using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogsHouseService.Infrastructure.Models.Entities;

namespace DogsHouseService.Infrastructure.Interfaces
{
    public interface IDogsRepository
    {
        Task<IList<Dog>> GetDogsAsync();
        Task<IList<Dog>> GetDogsAsync(int pageNumber, int pageSize);

        Task<Dog> GetByIdAsync(int id);
        Task<Dog?> CreateDogAsync(Dog dog);
        Task<Dog?> GetDogByNameAsync(string? name);
    }
}
