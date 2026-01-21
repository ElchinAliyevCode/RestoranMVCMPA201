using System.ComponentModel.DataAnnotations;

namespace RestoranMVCMPA201.ViewModels.AccountViewModels;

public class LoginVM
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
