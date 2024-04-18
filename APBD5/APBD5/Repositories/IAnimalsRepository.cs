using APBD5.Models;
using APBD5.Models.DTOs;

namespace APBD5.Repositories;

public interface IAnimalsRepository
{
    public IEnumerable<Animal>? GetAnimals(string orderBy="Name");
    public int CreateAnimal(AnimalDTO animal);
    public int DeleteAnimal(int idAnimal);
    public int UpdateAnimal(int idAnimal, AnimalDTO animal);
}