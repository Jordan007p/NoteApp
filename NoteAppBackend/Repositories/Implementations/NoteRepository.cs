using Microsoft.EntityFrameworkCore;
using NoteAppBackend.Data;
using NoteAppBackend.Models.DTOs;
using NoteAppBackend.Models.Entities;
using NoteAppBackend.Repositories.Interfaces;

namespace NoteAppBackend.Repositories.Implementations
{
    public class NoteRepository(AppDbContext context) : INoteRepository
    {
        /// <summary>
        /// Retrieves a note by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the note to retrieve.</param>
        /// <returns>A note if found, or null if no note exists with the given ID.</returns>
        public async Task<Note?> GetByIdAsync(int id)
        {
            return await context.Notes
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        /// <summary>
        /// Creates a new note entity.
        /// </summary>
        /// <param name="note">The note to be created.</param>
        /// <returns>The task result contains the created note entity.</returns>
        public async Task<Note> CreateAsync(Note note)
        {
            context.Notes.Add(note);
            await context.SaveChangesAsync();
            return note;
        }

        /// <summary>
        /// Updates an existing note with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the note to be updated.</param>
        /// <param name="updateNoteDto">The data transfer object containing updated values.</param>
        /// <return>The updated note if found; otherwise, null.</return>
        public async Task<Note?> UpdateAsync(int id, UpdateNoteDto updateNoteDto)
        {
            Note? existingNote = await context.Notes.FindAsync(id);

            if (existingNote == null)
            {
                return null;
            }

            existingNote.Title = updateNoteDto.Title;
            existingNote.Content = updateNoteDto.Content;

            await context.SaveChangesAsync();
            return existingNote;
        }

        /// <summary>
        /// Deletes a note with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the note to be deleted.</param>
        /// <returns>The task result is a boolean indicating whether the note was successfully deleted.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            int rowsAffected = await context.Notes
                .Where(n => n.Id == id)
                .ExecuteDeleteAsync();

            return rowsAffected > 0;
        }

        /// <summary>
        /// Retrieves a list of notes with only essential details.
        /// </summary>
        /// <returns>A collection of <see cref="ListItemNote"/> containing basic details of the notes.</returns>
        public async Task<IEnumerable<ListItemNote>> GetListItemsAsync()
        {
            return await context.Notes
                .AsNoTracking()
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new ListItemNote(n.Id, n.Title, n.CreatedAt))
                .ToListAsync();
        }
    }
}