using System.Threading.Tasks;

namespace GoalCalendar.UserIdentity.Data.Core.Users
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
    }
}
