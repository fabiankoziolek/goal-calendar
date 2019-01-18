using System;
using GoalCalendar.Core.Note.TimeRange;

namespace GoalCalendar.Core.Note.Web
{
    public class NoteRangeRequest
    {
        public int UserId { get; set; }
        public Range Range { get; set; }
        public DateTime DateTime { get; set; }
    }
}