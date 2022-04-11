using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IOwnerRepository
    {
        Owner GetOwnerById(int id);
        Owner GetOwnerByEmail(string email);
        List<Owner> GetAllOwners();
        void AddOwner(Owner owner);
        void UpdateOwner(Owner owner);
        void DeleteOwner(int ownerId);

    }
}
