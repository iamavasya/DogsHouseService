using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogsHouseService.BusinessLogic.Interfaces;
using DogsHouseService.Infrastructure.Models.Entities;
using DogsHouseService.Infrastructure.Models.DTOs;
using DogsHouseService.Infrastructure.Interfaces;

namespace DogsHouseService.BusinessLogic.Services
{
    public class DogService : IDogsService
    {
        private readonly IDogsRepository _dogsRepository;

        public DogService(IDogsRepository dogsRepository)
        {
            _dogsRepository = dogsRepository;
        }

        public async Task<IList<DogDto>> GetDogsAsync(int pageNumber, int pageSize, string attribute, string order)
        {
            var dogs = await _dogsRepository.GetDogsAsync(pageNumber, pageSize);
            return SortDogs(MapToDtoList(dogs), attribute, order);
        }

        public async Task<Dog> GetByIdAsync(int id)
        {
            return await _dogsRepository.GetByIdAsync(id);
        }

        public async Task<Dog?> GetDogByNameAsync(string? name)
        {
            return await _dogsRepository.GetDogByNameAsync(name);
        }

        public async Task<Dog?> CreateDogAsync(DogDto dog)
        {
            var dogEntity = MapToEntity(dog);
            return await _dogsRepository.CreateDogAsync(dogEntity);
        }

        public IList<DogDto> SortDogs(IList<DogDto> dogs, string attribute, string order)
        {
            // TODO: Not working.
            Func<DogDto, object> sortBy = attribute.ToLower() switch
            {
                "weight" => dog => dog.Weight,
                "taillength" => dog => dog.TailLength,
                "color" => dog => dog.Color,
                _ => dog => dog.Name
            };

            return order.ToLower() == "desc" ? dogs.OrderByDescending(sortBy).ToList() : dogs.OrderBy(sortBy).ToList();
        }

        public Dog MapToEntity(DogDto dog)
        {
            return new Dog
            {
                Name = dog.Name,
                Color = dog.Color,
                TailLength = dog.TailLength,
                Weight = dog.Weight
            };
        }

        public DogDto MapToDto(Dog dog)
        {
            return new DogDto
            {
                Name = dog.Name,
                Color = dog.Color,
                TailLength = dog.TailLength,
                Weight = dog.Weight
            };
        }

        public IList<DogDto> MapToDtoList(IList<Dog> dogs)
        {
            var dogDtoList = new List<DogDto>();

            foreach (var dog in dogs)
            {
                var dogDto = MapToDto(dog);
                dogDtoList.Add(dogDto);
            }

            return dogDtoList;
        }
    }
}
