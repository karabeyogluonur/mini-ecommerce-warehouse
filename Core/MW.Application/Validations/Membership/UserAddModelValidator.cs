using FluentValidation;
using MW.Application.Models.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application.Validations.Membership
{
    public class UserAddModelValidator : AbstractValidator<UserCreateModel>
    {
        public UserAddModelValidator()
        {
            
        }
    }
}
