using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoalCalendar.Core.Note.TimeRange.Strategies.Interface
{
    public interface IRangeStrategy
    {
        Task<IList<Note>> GetByRange(DateTime day, int id);
    }
}