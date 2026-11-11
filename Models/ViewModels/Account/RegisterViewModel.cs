using System;
using System.ComponentModel.DataAnnotations;

namespace RUNE.Models.ViewModels.Account;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    [Display(Name = "Kullanıcı adı")]
    public string UserName { get; set; } = string.Empty;
    [Required(ErrorMessage = "E-Posta boş bırakılamaz.")]
    [Display(Name = "E-Posta")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Lütfen geçerli bir şifre giriniz.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "Şifre tekrarı boş bırakılamaz")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Girdiğiniz şifreler birbiriyle eşleşmiyor.")]
    [Display(Name = "Şifre Tekrar")]
    public string ConfirmPassword { get; set; }= string.Empty;
}   
