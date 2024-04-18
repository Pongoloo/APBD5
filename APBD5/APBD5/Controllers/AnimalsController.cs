using System.Runtime.InteropServices.JavaScript;
using APBD5.Models;
using APBD5.Models.DTOs;
using APBD5.Services;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace APBD5.Controllers;

[ApiController]
[Route("api/animals")]
public class AnimalsController:ControllerBase
{
    private readonly IAnimalService _animalService;

    public AnimalsController(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    [HttpGet]
    public IActionResult GetAnimals(string orderBy="Name")
    {
        var enumerable = _animalService.GetAnimals();
        return Ok(enumerable);
    }


    [HttpPost]
    public IActionResult AddAnimal(AnimalDTO animalDto)
    {
        var animal = _animalService.CreateAnimal(animalDto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{idAnimal:int}")]
    public IActionResult ChangeAnimal(int idAnimal,AnimalDTO animalDto)
    {
        _animalService.UpdateAnimal(idAnimal, animalDto);
        return NoContent();
    }

    [HttpDelete("{idAnimal:int}")]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        _animalService.DeleteAnimal(idAnimal);
        return NoContent();
    }
}