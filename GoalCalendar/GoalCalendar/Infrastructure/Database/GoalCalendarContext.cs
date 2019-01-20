using GoalCalendar.Core.Note;
using GoalCalendar.Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.Infrastructure.Database
{
    public class GoalCalendarContext : DbContext
    {
        public GoalCalendarContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
    }
}