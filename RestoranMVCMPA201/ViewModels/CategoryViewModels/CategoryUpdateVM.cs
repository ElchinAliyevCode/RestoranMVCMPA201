using System.ComponentModel.DataAnnotations;

namespace RestoranMVCMPA201.ViewModels.CategoryViewModels;

public class CategoryUpdateVM
{
    public int Id { get; set; }
    [Required, MaxLength(256)]
    public string Name { get; set; }
}
