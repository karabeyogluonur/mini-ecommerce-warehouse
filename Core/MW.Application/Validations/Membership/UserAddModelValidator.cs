﻿using FluentValidation;
using MW.Application.Models.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Validations.Membership
{
    public class UserAddModelValidator : AbstractValidator<UserAddModel>
    {
        public UserAddModelValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().WithMessage("Ad boş geçilemez.")
                                           .NotNull().WithMessage("Ad boş geçilemez.");

            RuleFor(user => user.LastName).NotEmpty().WithMessage("Soyad boş geçilemez.")
                                           .NotNull().WithMessage("Soyad boş geçilemez.");

            RuleFor(user => user.Email).NotEmpty().WithMessage("Email boş geçilemez.")
                                           .NotNull().WithMessage("Email boş geçilemez.")
                                           .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(user => user.Password).NotEmpty().WithMessage("Şifre boş geçilemez.")
                                           .NotNull().WithMessage("Şifre boş geçilemez.")
                                           .Length(8, 30).WithMessage("Şifre 8 ile 30 karakter arasında olmalıdır.");

            RuleFor(user => user.ConfirmPassword).Equal(user => user.Password).When(user => user.Password != null).WithMessage("Şifre tekrarı uyuşmamakta. Tekrar deneyiniz");

        }
    }
}
