using System.Collections.Generic;
using System.Threading.Tasks;
using GoalCalendar.UserIdentity.Data.Core.Users.Web;

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