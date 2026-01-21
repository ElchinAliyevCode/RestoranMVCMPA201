namespace RestoranMVCMPA201.Models;

public class Food
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
}
