using Microsoft.AspNetCore.Identity;

namespace GoalCalendar.UserIdentity.Data.Core.Users
{
    public class User : IdentityUser<int>
    {
        public string RefreshToken { get; private set; }

        public void UpdateRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public void Update(User user)
        {
            
        }
    }
}
