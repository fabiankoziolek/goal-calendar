using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}