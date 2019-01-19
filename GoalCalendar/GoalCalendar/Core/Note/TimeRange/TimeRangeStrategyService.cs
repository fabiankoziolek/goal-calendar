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
        private readonly IList<ITimeRangeStrategy> _strategiesAggregate;

        public TimeRangeStrategyService(INotesRepository context)
        {
            _context = context;
            _strategiesAggregate = new List<ITimeRangeStrategy>();
            PopulateStrategiesAggregate();
        }

        public ITimeRangeStrategy GetStrategy(Range range)
        {
            return _strategiesAggregate.FirstOrDefault(s => s.Range.Equals(range));
        }

        private void PopulateStrategiesAggregate()
        {
            _strategiesAggregate.Add(DayStrategy.GetStrategy(_context));
            _strategiesAggregate.Add(WeekStrategy.GetStrategy(_context));
            _strategiesAggregate.Add(MonthStrategy.GetStrategy(_context));
            _strategiesAggregate.Add(YearStrategy.GetStrategy(_context));
        }
    }
}