using System.Threading.Tasks;

namespace GoalCalendar.UserIdentity.Data.Core.Users
{
    public interface IUserIdentityService
    {
        Task<User> GetById(int id);
    }
}
