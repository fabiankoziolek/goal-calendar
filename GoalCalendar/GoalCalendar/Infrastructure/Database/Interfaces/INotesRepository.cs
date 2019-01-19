using GoalCalendar.Core.Note;
using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.Infrastructure.Database.Interfaces
{
    public interface INotesRepository
    {
        DbSet<Note> Notes { get; set; }
    }
}