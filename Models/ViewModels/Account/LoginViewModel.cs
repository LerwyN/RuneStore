using System;
using System.ComponentModel.DataAnnotations;

namespace RUNE.Models.ViewModels.Account;

public class LoginViewModel
{
    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    [Display(Name = "Kullanıcı Adı")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-Posta boş bırakılamaz.")]
    [Display(Name = "E-Posta")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Şifre zorunludur")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; } = string.Empty;


}
