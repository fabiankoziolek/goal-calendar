using System.Collections.Generic;
using System.Threading.Tasks;
using GoalCalendar.Core.Note.Web;
using GoalCalendar.Utilities.AutomaticDI;

namespace GoalCalendar.Core.Note
{
    public interface INotesService : IScoped
    {
        Task<Note> Get(int id);
        Task<IList<Note>> Get(NoteRangeRequest request);
        Task Add(Note note);
        Task Update(Note note, int id);
        Task Delete(int id);
    }
}