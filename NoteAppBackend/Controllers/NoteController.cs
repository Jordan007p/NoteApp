using Microsoft.AspNetCore.Mvc;
using NoteAppBackend.Models.DTOs;
using NoteAppBackend.Services.Interfaces;

namespace NoteAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController(INoteService noteService) : ControllerBase
    {
        /// <summary>
        /// Retrieves a collection of notes containing summary information about each note for display in a list.
        /// </summary>
        /// <returns>A collection of <see cref="ListItemNote"/> containing the ID, title, and creation date of each note.</returns>
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

        /// <summary>
        /// Retrieves the details of a specific note by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the note to retrieve.</param>
        /// <returns>An <see cref="NoteDto"/> object containing the full note information</returns>
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

        /// <summary>
        /// Creates a new note with the specified title and content.
        /// </summary>
        /// <param name="request">The title and content.</param>
        /// <returns>A <see cref="NoteDto"/> object representing the created note</returns>
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

        /// <summary>
        /// Updates an existing note with the provided title and content.
        /// </summary>
        /// <param name="id">The unique identifier of the note to update.</param>
        /// <param name="request">An <see cref="UpdateNoteDto"/> containing the updated title and content for the note.</param>
        /// <returns>An <see cref="NoteDto"/> object representing the updated note, or null if the update operation fails.</returns>
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

        /// <summary>
        /// Deletes a note specified by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the note to delete.</param>
        /// <returns>Returns a no-content status if successful or a not-found status if the note does not exist.</returns>
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