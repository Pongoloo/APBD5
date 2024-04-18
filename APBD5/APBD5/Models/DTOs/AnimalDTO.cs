namespace APBD5.Models.DTOs;

public class AnimalDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Area { get; set; }

    public AnimalDTO(string name, string description, string category, string area)
    {
        Name = name;
        Description = description;
        Category = category;
        Area = area;
    }
}