using System;
using System.Threading.Tasks;

namespace GoalCalendar.Core.Note
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int NoteTypeId { get; set; }
        public DateTime DateTime { get; set; }

        public void Update(Note note)
        {
            Content = note.Content;
            Title = note.Title;
            DateTime = note.DateTime;
            NoteTypeId = note.NoteTypeId;
        }
    }
}