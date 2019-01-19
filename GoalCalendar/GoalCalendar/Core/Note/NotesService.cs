using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoalCalendar.Core.Note.TimeRange;
using GoalCalendar.Core.Note.TimeRange.Strategies.Interface;
using GoalCalendar.Core.Note.Web;
using GoalCalendar.Infrastructure.Database;
using GoalCalendar.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.Core.Note
{
    public class NotesService : INotesService
    {
        private readonly GoalCalendarContext _context;
        private ITimeRangeStrategy _timeRangeStrategy;
        private readonly ITimeRangeStrategiesService _timeRangeStrategyService;

        public NotesService(GoalCalendarContext context, ITimeRangeStrategiesService timeRangeStrategyService)
        {
            _context = context;
            _timeRangeStrategyService = timeRangeStrategyService;
        }

        public async Task<Note> Get(int id)
        {
            if (await Exists(id))
                return await _context.Notes.FirstOrDefaultAsync(n => n.Id.Equals(id));
            throw new ObjectNotFoundException("Not found", $"Note with id: {id} not found");
        }

        public async Task<IList<Note>> Get(NoteRangeRequest request)
        {
            _timeRangeStrategy = _timeRangeStrategyService.GetStrategy(request.Range);
            return await _timeRangeStrategy.GetByRange(request.DateTime, request.UserId);
        }

        public async Task Add(Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Update(Note note, int id)
        {
            if (!await Exists(id)) throw new ObjectNotFoundException("Not found", $"Note with id: {id} not found");
            var noteToUpdate = await Get(id);
            noteToUpdate.Update(note);
            _context.Notes.Update(note);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Delete(int id)
        {
            if (!await Exists(id)) throw new ObjectNotFoundException("Not found", $"Note with id: {id} not found");
            var noteToRemove = _context.Notes.FirstOrDefault(n => n.Id.Equals(id));
            _context.Notes.Remove(noteToRemove);
            _context.SaveChanges();
        }

        private async Task<bool> Exists(int id)
        {
            var exists = _context.Notes.AnyAsync(n => n.Id.Equals(id));
            return await exists;
        }

        private void SetRangeStrategy(ITimeRangeStrategy timeRangeStrategy)
        {
            _timeRangeStrategy = timeRangeStrategy;
        }
    }
}