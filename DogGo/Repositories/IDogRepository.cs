using DogGo.Controllers;
using DogGo.Models;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IDogRepository
    {

        List<Dog> GetAllDogs();
        Dog GetDogById(int id);

        public void AddDog(Dog dog);

        public void UpdateDog(Dog dog);

        public void DeleteDog(int id);
        List<Dog> GetDogsByOwnerId(int ownerId);

    }
}
