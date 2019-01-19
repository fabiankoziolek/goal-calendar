using System.Collections.Generic;
using System.Linq;
using GoalCalendar.Core.Note.TimeRange.Strategies;
using GoalCalendar.Core.Note.TimeRange.Strategies.Interface;
using GoalCalendar.Infrastructure.Database.Interfaces;


namespace GoalCalendar.Core.Note.TimeRange
{
    public class TimeRangeStrategyService : ITimeRangeStrategiesService
    {
        private readonly INotesRepository _context;
        private readonly IList<ITimeRangeStrategy> _strategies;

        public TimeRangeStrategyService(INotesRepository context)
        {
            _context = context;
            _strategies = new List<ITimeRangeStrategy>();
            PopulateStrategiesAggregate();
        }

        public ITimeRangeStrategy GetStrategy(Range range)
        {
            return _strategies.FirstOrDefault(s => s.Range.Equals(range));
        }

        private void PopulateStrategiesAggregate()
        {
            _strategies.Add(new DayStrategy(_context));
            _strategies.Add(new WeekStrategy(_context));
            _strategies.Add(new MonthStrategy(_context));
            _strategies.Add(new MonthStrategy(_context));
        }
    }
}