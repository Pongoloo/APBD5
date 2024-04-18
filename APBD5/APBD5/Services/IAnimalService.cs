using APBD5.Models;
using APBD5.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace APBD5.Services;

public interface IAnimalService
{
    public IEnumerable<Animal>? GetAnimals(string orderBy="Name");
    public int CreateAnimal(AnimalDTO animal);
    public int DeleteAnimal(int idAnimal);
    public int UpdateAnimal(int idAnimal, AnimalDTO animal);
}