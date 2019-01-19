using GoalCalendar.Core.Note.TimeRange.Strategies.Interface;
using GoalCalendar.Utilities.AutomaticDI;

namespace GoalCalendar.Core.Note.TimeRange
{
    public interface ITimeRangeStrategiesService : ISingleton
    {
        ITimeRangeStrategy GetStrategy(Range range);
    }
}