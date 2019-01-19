using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoalCalendar.Core.Note.TimeRange.Strategies.Interface
{
    public interface ITimeRangeStrategy
    {
        Range Range { get; set; }
        Task<IList<Note>> GetByRange(DateTime day, int id);
    }
}