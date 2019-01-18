using System;

namespace GoalCalendar.Core.Note.Web
{
    public class NoteRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int NoteTypeId { get; set; }
        public DateTime DateTime { get; set; }
    }
}