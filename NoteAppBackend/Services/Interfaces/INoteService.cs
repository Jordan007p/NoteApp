using NoteAppBackend.Models.DTOs;

namespace NoteAppBackend.Services.Interfaces
{
    public interface INoteService
    {
        Task<NoteDto?> GetNoteByIdAsync(int id);

        Task<NoteDto> CreateNoteAsync(CreateNoteDto createDto);

        Task<NoteDto?> UpdateNoteAsync(int id, UpdateNoteDto updateDto);

        Task<bool> DeleteNoteAsync(int id);

        Task<IEnumerable<ListItemNote>> GetAllListItemsAsync();
    }
}