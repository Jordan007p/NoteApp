using NoteAppBackend.Models.DTOs;
using NoteAppBackend.Models.Entities;

namespace NoteAppBackend.Repositories.Interfaces
{
    public interface INoteRepository
    {
        /// <summary>
        /// Retrieves a note by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the note to retrieve.</param>
        /// <returns>A note if found, or null if no note exists with the given ID.</returns>
        Task<Note?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new note entity.
        /// </summary>
        /// <param name="note">The note to be created.</param>
        /// <returns>The task result contains the created note entity.</returns>
        Task<Note> CreateAsync(Note note);

        /// <summary>
        /// Updates an existing note with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the note to be updated.</param>
        /// <param name="updateNoteDto">The data transfer object containing updated values.</param>
        /// <return>The updated note if found; otherwise, null.</return>
        Task<Note?> UpdateAsync(int id, UpdateNoteDto updateNoteDto);

        /// <summary>
        /// Deletes a note with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the note to be deleted.</param>
        /// <returns>The task result is a boolean indicating whether the note was successfully deleted.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves a list of notes with only essential details.
        /// </summary>
        /// <returns>A collection of <see cref="ListItemNote"/> containing basic details of the notes.</returns>
        Task<IEnumerable<ListItemNote>> GetListItemsAsync();
    }
}