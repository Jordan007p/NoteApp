using NoteApp.Models.Entities;

namespace NoteApp.Repositories.Interfaces;

public interface INoteRepository
{
    Task<Note?> GetByIdAsync(int id);
    Task<IEnumerable<Note>> GetAllAsync();
    Task<Note> CreateAsync(Note note);
    Task<Note> UpdateAsync(Note note);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}