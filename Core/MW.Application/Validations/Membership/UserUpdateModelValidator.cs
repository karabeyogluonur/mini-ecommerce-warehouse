using FluentValidation;
using MW.Application.Models.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Validations.Membership
{
    public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
    {
        public UserUpdateModelValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().WithMessage("Ad boş geçilemez.")
                                           .NotNull().WithMessage("Ad boş geçilemez.");

            RuleFor(user => user.LastName).NotEmpty().WithMessage("Soyad boş geçilemez.")
                                           .NotNull().WithMessage("Soyad boş geçilemez.");

            RuleFor(user => user.Email).NotEmpty().WithMessage("Email boş geçilemez.")
                                           .NotNull().WithMessage("Email boş geçilemez.")
                                           .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(user => user.Password).Length(8, 30).WithMessage("Şifre 8 ile 30 karakter arasında olmalıdır.")
                                           .When(user=>user.Password == null);

            RuleFor(user => user.ConfirmPassword).Equal(user => user.Password).When(user => user.Password != null).WithMessage("Şifre tekrarı uyuşmamakta. Tekrar deneyiniz");
        }
    }
}
