using System;
using System.ComponentModel.DataAnnotations;

namespace RUNE.Models.ViewModels.Account;

public class LoginViewModel
{
    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    [Display(Name = "Kullanıcı adı")]
    public string UserName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Şifre zorunludur")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; } = string.Empty;


}
