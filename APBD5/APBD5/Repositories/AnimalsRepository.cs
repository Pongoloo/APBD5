using APBD5.Models;
using APBD5.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace APBD5.Repositories;

public class AnimalsRepository:IAnimalsRepository
{
    private readonly IConfiguration _configuration;
    public AnimalsRepository(IConfiguration configuration)
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
    public IEnumerable<Animal>? GetAnimals(string orderBy="Name")
    {
        if (!IsParameterCorrect(orderBy))
        {
            return null;
        }
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        
        connection.Open();

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

        return resultList;
    }

    public int CreateAnimal(AnimalDTO animal)
    {
        using SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
        
        sqlConnection.Open();

        
        using SqlCommand command = new SqlCommand("INSERT INTO Animals VALUES(@Name,@Description,@Category,@Area)");
        command.Parameters.AddWithValue("@Name", animal.Name);
        command.Parameters.AddWithValue("@Description", animal.Description);
        command.Parameters.AddWithValue("@Category", animal.Category);
        command.Parameters.AddWithValue("@Area", animal.Area);

        command.Connection = sqlConnection;
        
        var affectedCount = command.ExecuteNonQuery();

        return affectedCount;
    }
    
    public int DeleteAnimal(int idAnimal)
    {
        using SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));

        sqlConnection.Open();

        using SqlCommand command = new SqlCommand($"DELETE FROM ANIMALS WHERE IdAnimal={idAnimal}");

        command.Connection = sqlConnection;

        return command.ExecuteNonQuery();
    }

    public int UpdateAnimal(int idAnimal, AnimalDTO animal)
    {
        using SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));

        sqlConnection.Open();
        
        using SqlCommand command = new SqlCommand("UPDATE ANIMALS SET " +
                                                  "Name=@Name," +
                                                  "Description=@Description," +
                                                  "Category=@Category," +
                                                  "Area=@Area" +
                                                  $" WHERE IdAnimal={idAnimal}");
        command.Parameters.AddWithValue("@Name", animal.Name);
        command.Parameters.AddWithValue("@Description", animal.Description);
        command.Parameters.AddWithValue("@Category", animal.Category);
        command.Parameters.AddWithValue("@Area", animal.Area);

        command.Connection = sqlConnection;

        return command.ExecuteNonQuery();
        
    }
}