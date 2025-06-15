using NoteAppBackend.Models.DTOs;
using NoteAppBackend.Models.Entities;

namespace NoteAppBackend.Repositories.Interfaces
{
    public interface INoteRepository
    {
        Task<Note?> GetByIdAsync(int id);

        Task<Note> CreateAsync(Note note);

        Task<Note?> UpdateAsync(int id, UpdateNoteDto updateNoteDto);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<ListItemNote>> GetListItemsAsync();
    }
}