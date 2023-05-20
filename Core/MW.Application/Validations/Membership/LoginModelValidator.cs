using FluentValidation;
using MW.Application.Models.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Validations.Membership
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(login => login.Email).NotEmpty().WithMessage("Email adresi boş olamaz!")
                                         .NotNull().WithMessage("Email adresi boş olamaz!")
                                         .EmailAddress().WithMessage("Geçerli bir email adresi giriniz!");

            RuleFor(login => login.Email).NotEmpty().WithMessage("Şifre boş olamaz!")
                                         .NotNull().WithMessage("Şifre boş olamaz!");


        }
    }
}
