using NoteAppBackend.Models.DTOs;

namespace NoteAppBackend.Services.Interfaces
{
    public interface INoteService
    {
        /// <summary>
        /// Retrieves a note by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the note.</param>
        /// <returns>A <see cref="NoteDto"/> object containing the note if found, otherwise null.</returns>
        Task<NoteDto?> GetNoteByIdAsync(int id);

        /// <summary>
        /// Creates a new note with the specified title and content.
        /// </summary>
        /// <param name="createDto">An object containing the title and content for the note to be created.</param>
        /// <returns>A <see cref="NoteDto"/> object representing the newly created note.</returns>
        Task<NoteDto> CreateNoteAsync(CreateNoteDto createDto);

        /// <summary>
        /// Updates an existing note with the specified ID using the provided update data.
        /// </summary>
        /// <param name="id">The unique identifier of the note to update.</param>
        /// <param name="updateDto">The data to update the note.</param>
        /// <returns>
        /// A <see cref="NoteDto"/> containing the updated note data if the update is successful;
        /// null if the note with the specified ID does not exist.
        /// </returns>
        Task<NoteDto?> UpdateNoteAsync(int id, UpdateNoteDto updateDto);

        /// <summary>
        /// Deletes a note with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the note to delete.</param>
        /// <returns>The task result is a boolean indicating whether the note was successfully deleted.</returns>
        Task<bool> DeleteNoteAsync(int id);

        /// <summary>
        /// Retrieves a collection of notes with essential details.
        /// </summary>
        /// <returns>The task result contains an enumerable collection of <see cref="ListItemNote"/> containing the note details.</returns>
        Task<IEnumerable<ListItemNote>> GetAllListItemsAsync();
    }
}