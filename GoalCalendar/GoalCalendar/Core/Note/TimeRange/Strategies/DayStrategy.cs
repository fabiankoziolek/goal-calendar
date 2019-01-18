using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoalCalendar.Core.Note.TimeRange.Strategies.Interface;
using GoalCalendar.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.Core.Note.TimeRange.Strategies
{
    public class DayStrategy : IRangeStrategy
    {
        private readonly GoalCalendarContext _db;

        public DayStrategy(GoalCalendarContext db)
        {
            _db = db;
        }

        public async Task<IList<Note>> GetByRange(DateTime day, int id)
        {
            return await _db.Notes
                .Where(n => n.UserId.Equals(id) && n.DateTime.Date.ToString("d").Equals(day.Date.ToString("d")))
                .ToListAsync();
        }
    }
}