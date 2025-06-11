using Microsoft.EntityFrameworkCore;
using NoteApp.Data;
using NoteApp.Models.Entities;
using NoteApp.Repositories.Interfaces;

namespace NoteApp.Repositories.Implementations;

public class NoteRepository(AppDbContext context) : INoteRepository
{
    public async Task<Note?> GetByIdAsync(int id)
    {
        return await context.Notes.FindAsync(id);
    }

    public async Task<IEnumerable<Note>> GetAllAsync()
    {
        return await context.Notes
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task<Note> CreateAsync(Note note)
    {
        note.CreatedAt = DateTime.UtcNow;
        note.UpdatedAt = DateTime.UtcNow;

        context.Notes.Add(note);
        await context.SaveChangesAsync();
        return note;
    }

    public async Task<Note> UpdateAsync(Note note)
    {
        note.UpdatedAt = DateTime.UtcNow;

        context.Notes.Update(note);
        await context.SaveChangesAsync();
        return note;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var note = await context.Notes.FindAsync(id);
        if (note == null) return false;

        context.Notes.Remove(note);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Notes.AnyAsync(n => n.Id == id);
    }
}