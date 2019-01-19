using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoalCalendar.Core.Note.TimeRange.Strategies.Interface;
using GoalCalendar.Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.Core.Note.TimeRange.Strategies
{
    public class WeekStrategy : ITimeRangeStrategy
    {
        private readonly INotesRepository _context;
        public Range Range { get; set; }

        public WeekStrategy(INotesRepository context)
        {
            Range = Range.Week;
            _context = context;
        }

        public async Task<IList<Note>> GetByRange(DateTime day, int id)
        {
            return await _context.Notes
                .Where(n => n.UserId.Equals(id) &&
                            n.DateTime.Date > day.Date.AddDays(-(int) day.DayOfWeek) &&
                            n.DateTime.Date < day.Date.AddDays(6 - (int) day.DayOfWeek)).ToListAsync().ConfigureAwait(false);
        }
    }
}