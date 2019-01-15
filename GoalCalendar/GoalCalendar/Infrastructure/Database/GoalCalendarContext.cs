using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.Infrastructure.Database
{
    public class GoalCalendarContext : DbContext
    {
        public GoalCalendarContext(DbContextOptions options) : base(options)
        {
        }
    }
}