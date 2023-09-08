using FluentValidation;

namespace RestaurantApi.Models.Validators
{
    public class ModifyRestaurantDtoValidator : AbstractValidator<ModifyRestaurantDto>
    {
        public ModifyRestaurantDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(25);
        }
    }
}
