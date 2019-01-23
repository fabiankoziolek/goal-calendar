using FluentValidation.TestHelper;
using GoalCalendar.Core.Identity.Validators;
using Xunit;

namespace GoalCalendarTest
{
    public class UserRegistrationRequestValidationTest
    {
        private readonly UserRegistrationRequestValidator _validator;
        private const string ShortEmail = "xd";
        private const string ValidEmail = "alex1toryanik@gmail.com";

        public UserRegistrationRequestValidationTest()
        {
            _validator = new UserRegistrationRequestValidator();
        }

        [Fact]
        public void EmailIsNull_HaveValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(userRequest => userRequest.Email, (string)null);
        }

        [Fact]
        public void EmailIsTooShort_HaveValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(userRequest => userRequest.Email, ShortEmail);
        }

        [Fact]
        public void EmailIsValid_HaveNoValidationErrors()
        {
            _validator.ShouldNotHaveValidationErrorFor(userRequest => userRequest.Email, ValidEmail);
        }
    }
}
