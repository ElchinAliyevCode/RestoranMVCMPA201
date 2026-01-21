using System.ComponentModel.DataAnnotations;

namespace RestoranMVCMPA201.ViewModels.FoodViewModels;

public class FoodCreateVM
{
    [Required,MaxLength(256)]
    public string Name { get; set; }
    [Required, MaxLength(512)]
    public string Description { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public IFormFile Image { get; set; }
}
