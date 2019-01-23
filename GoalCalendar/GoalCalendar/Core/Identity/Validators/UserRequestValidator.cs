using FluentValidation;
using GoalCalendar.Core.Identity.Web;
using GoalCalendar.Utilities.AutomaticDI;

namespace GoalCalendar.Core.Identity.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequest>, ITransient
    {
        public UserRequestValidator()
        {
            RuleFor(ur => ur.Password).NotNull();
            RuleFor(ur => ur.Password).Length(5, 50);
        }
    }
}
