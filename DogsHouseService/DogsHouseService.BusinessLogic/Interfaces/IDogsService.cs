using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogsHouseService.Infrastructure.Models.Entities;
using DogsHouseService.Infrastructure.Models.DTOs;

namespace DogsHouseService.BusinessLogic.Interfaces
{
    public interface IDogsService
    {
        Task<IList<DogDto>> GetDogsAsync(int pageNumber, int pageSize, string attribute, string order);
        IList<Dog> SortDogs(IList<Dog> dogs, string attribute, string order);
        Task<Dog> GetByIdAsync(int id);
        Task<Dog?> CreateDogAsync(DogDto dog);
        Dog MapToEntity(DogDto dog);
        DogDto MapToDto(Dog dog);
        IList<DogDto> MapToDtoList(IList<Dog> dogs);
        Task<Dog?> GetDogByNameAsync(string? name);
    }
}
