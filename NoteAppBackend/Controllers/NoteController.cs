using Microsoft.AspNetCore.Mvc;
using NoteAppBackend.Models.DTOs;
using NoteAppBackend.Services.Interfaces;

namespace NoteAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController(INoteService noteService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListItemNote>>> GetAllListItems()
        {
            try
            {
                IEnumerable<ListItemNote> notes = await noteService.GetAllListItemsAsync();
                return Ok(notes);
            }
            catch
            {
                return StatusCode(500, "Failed to retrieve notes");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<NoteDto>> GetNote(int id)
        {
            try
            {
                NoteDto? note = await noteService.GetNoteByIdAsync(id);
                return note == null ? NotFound() : Ok(note);
            }
            catch
            {
                return StatusCode(500, "Failed to retrieve note");
            }
        }

        [HttpPost]
        public async Task<ActionResult<NoteDto>> CreateNote(CreateNoteDto request)
        {
            try
            {
                NoteDto createdNote = await noteService.CreateNoteAsync(request);
                return CreatedAtAction(nameof(GetNote), new { id = createdNote.Id }, createdNote);
            }
            catch
            {
                return StatusCode(500, "Failed to create note");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<NoteDto?>> UpdateNote(int id, UpdateNoteDto request)
        {
            try
            {
                NoteDto? updatedNote = await noteService.UpdateNoteAsync(id, request);
                return Ok(updatedNote);
            }
            catch
            {
                return StatusCode(500, "Failed to update note");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteNote(int id)
        {
            try
            {
                bool deleted = await noteService.DeleteNoteAsync(id);
                return deleted ? NoContent() : NotFound();
            }
            catch
            {
                return StatusCode(500, "Failed to delete note");
            }
        }
    }
}