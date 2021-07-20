using BeerCup.ApplicationServices.API.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Validators
{
    public class AddBreweryRequestValidator : AbstractValidator<AddBreweryRequest>
    {
        public AddBreweryRequestValidator()
        {
            this.RuleFor(b => b.Name).Length(3, 50);
            this.RuleFor(b => b.Name).NotEmpty();
        }
    }
}
