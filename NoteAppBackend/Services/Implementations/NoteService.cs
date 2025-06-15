using NoteAppBackend.Models.DTOs;
using NoteAppBackend.Models.Entities;
using NoteAppBackend.Repositories.Interfaces;
using NoteAppBackend.Services.Interfaces;

namespace NoteAppBackend.Services.Implementations
{
    public class NoteService(INoteRepository noteRepository) : INoteService
    {
        /// <summary>
        /// Retrieves a note by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the note.</param>
        /// <returns>A <see cref="NoteDto"/> object containing the note if found, otherwise null.</returns>
        public async Task<NoteDto?> GetNoteByIdAsync(int id)
        {
            Note? note = await noteRepository.GetByIdAsync(id);
            return note == null ? null : MapToDto(note);
        }

        /// <summary>
        /// Creates a new note with the specified title and content.
        /// </summary>
        /// <param name="createDto">An object containing the title and content for the note to be created.</param>
        /// <returns>A <see cref="NoteDto"/> object representing the newly created note.</returns>
        public async Task<NoteDto> CreateNoteAsync(CreateNoteDto createDto)
        {
            Note note = new() { Title = createDto.Title, Content = createDto.Content };

            Note createdNote = await noteRepository.CreateAsync(note);
            return MapToDto(createdNote);
        }

        /// <summary>
        /// Updates an existing note with the specified ID using the provided update data.
        /// </summary>
        /// <param name="id">The unique identifier of the note to update.</param>
        /// <param name="updateDto">The data to update the note.</param>
        /// <returns>
        /// A <see cref="NoteDto"/> containing the updated note data if the update is successful;
        /// null if the note with the specified ID does not exist.
        /// </returns>
        public async Task<NoteDto?> UpdateNoteAsync(int id, UpdateNoteDto updateDto)
        {
            Note? updatedNote = await noteRepository.UpdateAsync(id, updateDto);
            return updatedNote == null ? null : MapToDto(updatedNote);
        }

        /// <summary>
        /// Deletes a note with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the note to delete.</param>
        /// <returns>The task result is a boolean indicating whether the note was successfully deleted.</returns>
        public async Task<bool> DeleteNoteAsync(int id)
        {
            return await noteRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Retrieves a collection of notes with essential details.
        /// </summary>
        /// <returns>The task result contains an enumerable collection of <see cref="ListItemNote"/> containing the note details.</returns>
        public async Task<IEnumerable<ListItemNote>> GetAllListItemsAsync()
        {
            return await noteRepository.GetListItemsAsync();
        }

        private static NoteDto MapToDto(Note note)
        {
            return new NoteDto(
                note.Id,
                note.Title,
                note.Content,
                note.CreatedAt,
                note.UpdatedAt
            );
        }
    }
}