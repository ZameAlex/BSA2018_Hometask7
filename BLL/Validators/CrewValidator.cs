using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using BSA2018_Hometask4.Shared.DTO;

namespace BSA2018_Hometask4.BLL.Validators
{
    public class CrewValidator:AbstractValidator<CrewDto>
    {
        public CrewValidator()
        {
            RuleFor(f => f.ID).Empty();
            RuleFor(c => c.Pilot).NotNull();
            RuleFor(c => c.Stewardess).NotNull();
            RuleForEach(c => c.Stewardess).NotNull();
        }
    }
}
