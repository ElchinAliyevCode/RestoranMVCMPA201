using System.ComponentModel.DataAnnotations;

namespace RestoranMVCMPA201.ViewModels.CategoryViewModels;

public class CategoryCreateVM
{
    [Required, MaxLength(256)]
    public string Name { get; set; }
}

