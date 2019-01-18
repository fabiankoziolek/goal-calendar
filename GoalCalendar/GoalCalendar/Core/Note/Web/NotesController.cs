using System.Collections.Generic;
using AutoMapper;
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

        [HttpGet("id")]
        public ActionResult<NoteResponse> GetById(int id)
        {
            var note = _notesService.Get(id);
            return _mapper.Map<NoteResponse>(note);
        }

        [HttpGet("range")]
        public ActionResult<IList<NoteResponse>> GetByRange(NoteRangeRequest rangeRequest)
        {
            var notes = _notesService.Get(rangeRequest);
            return Ok(_mapper.Map<IList<NoteResponse>>(notes));
        }

        [HttpPost]
        public ActionResult Add(NoteRequest note)
        {
            var newNote = _mapper.Map<Note>(note);
            _notesService.Add(newNote);
            return NoContent();
        }

        [HttpPut("id")]
        public ActionResult Update(int id, NoteRequest note)
        {
            var noteToUpdate = _mapper.Map<Note>(note);
            _notesService.Update(noteToUpdate, id);
            return NoContent();
        }

        [HttpDelete("id")]
        public ActionResult Delete(int id)
        {
            _notesService.Delete(id);
            return NoContent();
        }
    }
}