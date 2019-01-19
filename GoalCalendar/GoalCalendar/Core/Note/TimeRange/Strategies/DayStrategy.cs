using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoalCalendar.Core.Note.TimeRange.Strategies.Interface;
using GoalCalendar.Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.Core.Note.TimeRange.Strategies
{
    public class DayStrategy : ITimeRangeStrategy
    {
        private readonly INotesRepository _context;
        public Range Range { get; set; }

        public DayStrategy(INotesRepository context)
        {
            _context = context;
            Range = Range.Day;
        }

        public async Task<IList<Note>> GetByRange(DateTime day, int id)
        {
            return await _context.Notes
                .Where(n => n.UserId.Equals(id) && n.DateTime.Date.ToString("d").Equals(day.Date.ToString("d")))
                .ToListAsync();
        }
    }
}