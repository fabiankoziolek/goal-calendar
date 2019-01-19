using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoalCalendar.Core.Note.TimeRange.Strategies.Interface;
using GoalCalendar.Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.Core.Note.TimeRange.Strategies
{
    public class YearStrategy : ITimeRangeStrategy
    {
        private readonly INotesRepository _context;
        private static YearStrategy _yearStrategy;
        public Range Range { get; set; }

        private YearStrategy(INotesRepository context)
        {
            Range = Range.Year;
            _context = context;
        }


        public async Task<IList<Note>> GetByRange(DateTime day, int id)
        {
            return await _context.Notes
                .Where(n => n.UserId.Equals(id) &&
                            n.DateTime.Date > day.Date.AddDays(-day.DayOfYear) &&
                            n.DateTime.Date < day.Date.AddDays(6 - day.DayOfYear)).ToListAsync();
        }

        public static ITimeRangeStrategy GetStrategy(INotesRepository context)
        {
            return _yearStrategy ?? (_yearStrategy = new YearStrategy(context));
        }
    }
}