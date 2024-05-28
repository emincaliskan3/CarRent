using System.ComponentModel.DataAnnotations;

public class ResetPasswordViewModel
{
    public string Email { get; set; }
    public string Token { get; set; }

    [Display(Name = "Yeni Şifre")]
    [Required(ErrorMessage = "Yeni şifre gereklidir.")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Display(Name = "Yeni Şifreyi Onayla")]
    [Required(ErrorMessage = "Yeni şifre onayı gereklidir.")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor.")]
    public string ConfirmPassword { get; set; }
}
