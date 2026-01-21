using System.ComponentModel.DataAnnotations;

namespace RestoranMVCMPA201.ViewModels.FoodViewModels;

public class FoodUpdateVM
{
    public int Id { get; set; }
    [Required, MaxLength(256)]
    public string Name { get; set; }
    [Required, MaxLength(512)]
    public string Description { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public IFormFile? Image { get; set; }
}
