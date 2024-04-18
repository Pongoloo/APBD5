using APBD5.Models;
using APBD5.Models.DTOs;
using APBD5.Repositories;

namespace APBD5.Services;

public class AnimalService:IAnimalService
{
    private readonly IAnimalsRepository _animalsRepository;

    public AnimalService(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }

    public IEnumerable<Animal>? GetAnimals(string orderBy = "Name")
    {
        return _animalsRepository.GetAnimals(orderBy);
    }

    public int CreateAnimal(AnimalDTO animal)
    {
        return _animalsRepository.CreateAnimal(animal);
    }

    public int DeleteAnimal(int idAnimal)
    {
        return _animalsRepository.DeleteAnimal(idAnimal);
    }

    public int UpdateAnimal(int idAnimal, AnimalDTO animal)
    {
        return _animalsRepository.UpdateAnimal(idAnimal, animal);
    }
}