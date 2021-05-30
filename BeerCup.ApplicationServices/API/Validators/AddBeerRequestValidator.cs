using BeerCup.ApplicationServices.API.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.API.Validators
{
    public class AddBeerRequestValidator : AbstractValidator<AddBeerRequest>
    {
        public AddBeerRequestValidator()
        {
            this.RuleFor(b => b.BattleId).GreaterThan(0);
        }
    }
}
