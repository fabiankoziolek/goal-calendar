using System.Threading.Tasks;
using GoalCalendar.UserIdentity.Data.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.UserIdentity.Data.Core.Users
{
    public class UserService
    {
        private readonly UserDbContext _context;

        public UserService(UserDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(int id)
        {
            if (await UserExists(id))
                return await _context.Users.FindAsync(id).ConfigureAwait(false);

            return null;
        }

        private Task<bool> UserExists(int id)
        {
            return _context.Users.AnyAsync(x => x.Id.Equals(id));
        }
    }
}
