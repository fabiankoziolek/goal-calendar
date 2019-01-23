using System.Threading.Tasks;

namespace GoalCalendar.UserIdentity.Data.Core.Users
{
    public interface IUsersService
    {
        Task<User> GetById(int id);
        Task Add(User user);
        Task Update(User user, int id);
        Task Delete(int id);
    }
}