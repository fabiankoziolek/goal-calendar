using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoalCalendar.Core.Note.TimeRange;
using GoalCalendar.Core.Note.TimeRange.Strategies;
using GoalCalendar.Core.Note.TimeRange.Strategies.Interface;
using GoalCalendar.Core.Note.Web;
using GoalCalendar.Infrastructure.Database;
using GoalCalendar.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.Core.Note
{
    public class NotesService : INotesService
    {
        private readonly GoalCalendarContext _db;
        private IRangeStrategy _rangeStrategy;

        public NotesService(GoalCalendarContext db)
        {
            _db = db;
        }

        public async Task<Note> Get(int id)
        {
            if (await Exists(id))
                return await _db.Notes.FirstOrDefaultAsync(n => n.Id.Equals(id));
            throw new ObjectNotFoundException("Not found", $"Note with id: {id} not found");
        }

        public async Task<IList<Note>> Get(NoteRangeRequest request)
        {
            SetRangeStrategy(request.Range);
            return await _rangeStrategy.GetByRange(request.DateTime, request.UserId);
        }

        public async Task Add(Note note)
        {
            await _db.Notes.AddAsync(note);
            await _db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Update(Note note, int id)
        {
            if (!await Exists(id)) throw new ObjectNotFoundException("Not found", $"Note with id: {id} not found");
            var noteToUpdate = await Get(id);
            noteToUpdate.Update(note);
            _db.Notes.Update(note);
            await _db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Delete(int id)
        {
            if (!await Exists(id)) throw new ObjectNotFoundException("Not found", $"Note with id: {id} not found");
            var noteToRemove = _db.Notes.FirstOrDefault(n => n.Id.Equals(id));
            _db.Notes.Remove(noteToRemove);
            _db.SaveChanges();
        }

        private async Task<bool> Exists(int id)
        {
            var exists = _db.Notes.AnyAsync(n => n.Id.Equals(id));
            return await exists;
        }


        private void SetRangeStrategy(Range range)
        {
            switch (range)
            {
                case Range.Day:
                    _rangeStrategy = new DayStrategy(_db);
                    break;
                case Range.Week:
                    _rangeStrategy = new WeekStrategy(_db);
                    break;
                case Range.Month:
                    _rangeStrategy = new MonthStrategy(_db);
                    break;
                case Range.Year:
                    _rangeStrategy = new YearStrategy(_db);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(range), range, null);
            }
        }
    }
}