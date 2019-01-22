using System.Collections.Generic;
using System.Threading.Tasks;
using GoalCalendar.UserIdentity.Data.Core.Users.Web;
using GoalCalendar.UserIdentity.Data.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.UserIdentity.Data.Core.Users
{
    public class UserService : IUserIdentityService , IUsersService
    {
        private readonly UserDbContext _context;

        public UserService(UserDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetById(int id)
        {
            if (await UserExists(id))
                return await _context.Users.FindAsync(id).ConfigureAwait(false);
            return null;
        }

        private Task<bool> UserExists(int id)
        {
            return _context.Users.AnyAsync(x => x.Id.Equals(id));
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Update(User user, int id)
        {
            var userToUpdate = await GetById(id);
            userToUpdate.Update(user);
            _context.Update(userToUpdate);
            _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Delete(int id)
        {
            if (await UserExists(id))
            {
                var userToDelete = await GetById(id);
                _context.Users.Remove(userToDelete);
            } // TODO: FIX ONE DAY, SOMEDAY, NEVER
        }
    }
}
