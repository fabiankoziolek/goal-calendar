using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoalCalendar.Core.Note.TimeRange;
using Microsoft.AspNetCore.Mvc;

namespace GoalCalendar.Core.Note.Web
{
    [Route("api/[controller]"), ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _notesService;
        private readonly IMapper _mapper;

        public NotesController(INotesService notesService, IMapper mapper)
        {
            _notesService = notesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<NoteResponse> GetById(int id)
        {
            var note = await _notesService.Get(id);
            return _mapper.Map<NoteResponse>(note);
        }

        [HttpGet("/range")]
        public async Task<IList<NoteResponse>> GetByRange(DateTime dateTime, Range range, int userId)
        {
            var rangeRequest = new NoteRangeRequest()
            {
                DateTime = dateTime,
                Range = range,
                UserId = userId
            };
            var notes = await _notesService.Get(rangeRequest);
            return _mapper.Map<IList<NoteResponse>>(notes);
        }

        [HttpPost]
        public async Task<IActionResult> Add(NoteRequest note)
        {
            var newNote = _mapper.Map<Note>(note);
            await _notesService.Add(newNote);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NoteRequest note)
        {
            var noteToUpdate = _mapper.Map<Note>(note);
            await _notesService.Update(noteToUpdate, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _notesService.Delete(id);
            return NoContent();
        }
    }
}