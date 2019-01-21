using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoalCalendar.Core.Identity.Web
{
    public class UserRegistrationRequest : UserRequest
    {
        public string Email { get; set; }
    }
}
