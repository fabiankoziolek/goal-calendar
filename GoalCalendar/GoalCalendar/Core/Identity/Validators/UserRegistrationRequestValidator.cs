using FluentValidation;
using GoalCalendar.Core.Identity.Web;
using GoalCalendar.Utilities.AutomaticDI;

namespace GoalCalendar.Core.Identity.Validators
{
    public class UserRegistrationRequestValidator : AbstractValidator<UserRegistrationRequest>, ITransient
    {
        public UserRegistrationRequestValidator()
        {
            Include(new UserRequestValidator());
            RuleFor(urr => urr.Email).NotNull();
            RuleFor(urr => urr.Email).Length(7, 230);
        }
    }
}
