using GoalCalendar.Core.Note;
using GoalCalendar.UserIdentity.Data.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.Infrastructure.Database
{
    public class GoalCalendarContext : UserDbContext
    {
        public GoalCalendarContext(DbContextOptions options) : base(options)
        {
             
        }

        public DbSet<Note> Notes { get; set; }
    }
}