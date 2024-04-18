using System.Runtime.InteropServices.JavaScript;
using APBD5.Models;
using APBD5.Models.DTOs;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace APBD5.Controllers;

[ApiController]
[Route("api/animals")]
public class AnimalsController:ControllerBase
{
    private readonly IConfiguration _configuration;
    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private bool IsParameterCorrect(string? columnName)
    {
        if (columnName == null)
        {
            return true;
        }
        return (columnName.Equals("Name") ||
                columnName.Equals("Description") ||
                columnName.Equals("Category") ||
                columnName.Equals("Area"));
    }
    [HttpGet]
    public IActionResult GetAnimals(string orderBy="Name")
    {
        if (!IsParameterCorrect(orderBy))
        {
            return BadRequest("Wrong input in my parameter");
        }
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        
        connection.Open();

        Console.WriteLine(orderBy.Equals("Name"));
        using SqlCommand command =
            new SqlCommand($"SELECT * FROM ANIMALS ORDER BY {orderBy}");

        
        
        command.Connection = connection;
        var sqlDataReader = command.ExecuteReader();

        List<Animal> resultList = new List<Animal>();

        var ordinalId = sqlDataReader.GetOrdinal("IdAnimal");
        var ordinalName = sqlDataReader.GetOrdinal("Name");
        var ordinalDescription = sqlDataReader.GetOrdinal("Description");
        var ordinalCategory = sqlDataReader.GetOrdinal("Category");
        var ordinalArea = sqlDataReader.GetOrdinal("Area");
        
        while (sqlDataReader.Read())
        {
            Animal animal = new Animal(
                sqlDataReader.GetInt32(ordinalId),
                sqlDataReader.GetString(ordinalName),
                sqlDataReader.GetString(ordinalDescription),
                sqlDataReader.GetString(ordinalCategory),
                sqlDataReader.GetString(ordinalArea));
            resultList.Add(animal);
        }

        return Ok(resultList);
    }


    [HttpPost]
    public IActionResult AddAnimal(AnimalDTO animalDto)
    {
        using SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
        
        sqlConnection.Open();

        
        using SqlCommand command = new SqlCommand("INSERT INTO Animals VALUES(@Name,@Description,@Category,@Area)");
        command.Parameters.AddWithValue("@Name", animalDto.Name);
        command.Parameters.AddWithValue("@Description", animalDto.Description);
        command.Parameters.AddWithValue("@Category", animalDto.Category);
        command.Parameters.AddWithValue("@Area", animalDto.Area);

        command.Connection = sqlConnection;
        
        var executeNonQuery = command.ExecuteNonQuery();

        return Created("", null);
        
    }

    [HttpPut("{idAnimal:int}")]
    public IActionResult changeAnimal(int idAnimal,AnimalDTO animalDto)
    {
        using SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));

        sqlConnection.Open();
        
        using SqlCommand command = new SqlCommand("UPDATE ANIMALS SET " +
                                                  "Name=@Name," +
                                                  "Description=@Description," +
                                                  "Category=@Category," +
                                                  "Area=@Area" +
                                                  $" WHERE IdAnimal={idAnimal}");
        command.Parameters.AddWithValue("@Name", animalDto.Name);
        command.Parameters.AddWithValue("@Description", animalDto.Description);
        command.Parameters.AddWithValue("@Category", animalDto.Category);
        command.Parameters.AddWithValue("@Area", animalDto.Area);

        command.Connection = sqlConnection;

        command.ExecuteNonQuery();
        return NoContent();
    }

    [HttpDelete("{idAnimal:int}")]
    public IActionResult deleteAnimal(int idAnimal)
    {
        using SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));

        sqlConnection.Open();

        using SqlCommand command = new SqlCommand($"DELETE FROM ANIMALS WHERE IdAnimal={idAnimal}");

        command.Connection = sqlConnection;

        command.ExecuteNonQuery();
        return NoContent();
    }
}