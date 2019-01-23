using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.TestHelper;
using GoalCalendar.Core.Identity.Validators;
using Xunit;

namespace GoalCalendarTest
{
    public class UserRequestValidationTest
    {
        private readonly UserRequestValidator _validator;
        private const string ShortPassword = "xd";
        private const string ValidPassword = "blahblahblah";

        public UserRequestValidationTest()
        {
            _validator = new UserRequestValidator();
        }

        [Fact]
        public void PasswordIsNull_HaveValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(userRequest => userRequest.Password, (string) null);
        }

        [Fact]
        public void PasswordIsTooShort_HaveValidationErrors()
        {
            _validator.ShouldHaveValidationErrorFor(userRequest => userRequest.Password, ShortPassword);
        }

        [Fact]
        public void PasswordIsValid_HaveNoValidationErrors()
        {
            _validator.ShouldNotHaveValidationErrorFor(userRequest => userRequest.Password, ValidPassword);
        }
    }
}
