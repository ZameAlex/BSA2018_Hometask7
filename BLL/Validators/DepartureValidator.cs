using BSA2018_Hometask4.Shared.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSA2018_Hometask4.BLL.Validators
{
    public class DepartureValidator : AbstractValidator<DepartureDto>
    {
        public DepartureValidator()
        {
            RuleFor(f => f.ID).Empty();
            RuleFor(d => d.Number).NotNull().NotEmpty();
            RuleFor(d => d.Date).NotNull().NotEmpty();
            RuleFor(d => d.Crew).NotNull();
            RuleFor(d => d.Plane).NotNull();
        }
    }
}