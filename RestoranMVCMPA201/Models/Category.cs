namespace RestoranMVCMPA201.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Food> Foods { get; set; } = new List<Food>();
}
