namespace APBD5.Models;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Area { get; set; }

    public Animal(int id,string name, string description, string category, string area)
    {
        Id = id;
        Name = name;
        Description = description;
        Category = category;
        Area = area;
    }
}