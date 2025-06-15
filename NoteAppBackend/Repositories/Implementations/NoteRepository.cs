using Microsoft.EntityFrameworkCore;
using NoteAppBackend.Data;
using NoteAppBackend.Models.DTOs;
using NoteAppBackend.Models.Entities;
using NoteAppBackend.Repositories.Interfaces;

namespace NoteAppBackend.Repositories.Implementations
{
    public class NoteRepository(AppDbContext context) : INoteRepository
    {
        public async Task<Note?> GetByIdAsync(int id)
        {
            return await context.Notes
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            return await context.Notes
                .AsNoTracking()
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<Note> CreateAsync(Note note)
        {
            context.Notes.Add(note);
            await context.SaveChangesAsync();
            return note;
        }

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

        public async Task<bool> DeleteAsync(int id)
        {
            int rowsAffected = await context.Notes
                .Where(n => n.Id == id)
                .ExecuteDeleteAsync();

            return rowsAffected > 0;
        }

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