using NoteApp.Models.DTOs;

namespace NoteApp.Services.Interfaces;

public interface INoteService
{
    Task<NoteDto?> GetNoteByIdAsync(int id);
    Task<IEnumerable<NoteDto>> GetAllNotesAsync();
    Task<NoteDto> CreateNoteAsync(CreateNoteDto createDto);
    Task<NoteDto?> UpdateNoteAsync(int id, UpdateNoteDto updateDto);
    Task<bool> DeleteNoteAsync(int id);
}