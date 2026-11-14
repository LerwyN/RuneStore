using Microsoft.AspNetCore.Identity;

namespace RUNE.Services.Identity;

public sealed class TurkishIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() => CreateError("Bilinmeyen bir hata oluştu.");

    public override IdentityError InvalidUserName(string? userName) =>
        CreateError("Geçerli bir kullanıcı adı giriniz.");

    public override IdentityError InvalidEmail(string? email) =>
        CreateError("Geçerli bir e-posta adresi giriniz.");

    public override IdentityError DuplicateUserName(string userName) =>
        CreateError("Bu kullanıcı adı zaten kullanılıyor.");

    public override IdentityError DuplicateEmail(string email) =>
        CreateError("Bu e-posta adresi zaten kayıtlı.");

    public override IdentityError PasswordMismatch() =>
        CreateError("Girdiğiniz şifre doğru değil.");

    public override IdentityError PasswordTooShort(int length) =>
        CreateError($"Şifre en az {length} karakter olmalıdır.");

    public override IdentityError PasswordRequiresNonAlphanumeric() =>
        CreateError("Şifre en az bir sembol içermelidir.");

    public override IdentityError PasswordRequiresDigit() =>
        CreateError("Şifre en az bir sayı içermelidir.");

    public override IdentityError PasswordRequiresUpper() =>
        CreateError("Şifre en az bir büyük harf içermelidir.");

    public override IdentityError PasswordRequiresLower() =>
        CreateError("Şifre en az bir küçük harf içermelidir.");

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) =>
        CreateError($"Şifre en az {uniqueChars} farklı karakter içermelidir.");

    private static IdentityError CreateError(string description) => new()
    {
        Description = description
    };
}
