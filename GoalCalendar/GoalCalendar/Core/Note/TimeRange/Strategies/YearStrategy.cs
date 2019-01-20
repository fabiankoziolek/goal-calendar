using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoalCalendar.Core.Note.TimeRange.Strategies.Interface;
using GoalCalendar.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.Core.Note.TimeRange.Strategies
{
    public class YearStrategy : ITimeRangeStrategy
    {
        private readonly GoalCalendarContext _context;
        public Range Range { get; set; }

        public YearStrategy(GoalCalendarContext context)
        {
            Range = Range.Year;
            _context = context;
        }

        public async Task<IList<Note>> GetByRange(DateTime day, int id)
        {
            return await _context.Notes
                .Where(n => n.UserId.Equals(id) && n.DateTime.Year == day.Year).ToListAsync();
        }
    }
}