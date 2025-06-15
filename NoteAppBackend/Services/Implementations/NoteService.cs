using NoteAppBackend.Models.DTOs;
using NoteAppBackend.Models.Entities;
using NoteAppBackend.Repositories.Interfaces;
using NoteAppBackend.Services.Interfaces;

namespace NoteAppBackend.Services.Implementations
{
    public class NoteService(INoteRepository noteRepository) : INoteService
    {
        public async Task<NoteDto?> GetNoteByIdAsync(int id)
        {
            Note? note = await noteRepository.GetByIdAsync(id);
            return note == null ? null : MapToDto(note);
        }

        public async Task<NoteDto> CreateNoteAsync(CreateNoteDto createDto)
        {
            Note note = new() { Title = createDto.Title, Content = createDto.Content };

            Note createdNote = await noteRepository.CreateAsync(note);
            return MapToDto(createdNote);
        }

        public async Task<NoteDto?> UpdateNoteAsync(int id, UpdateNoteDto updateDto)
        {
            Note? updatedNote = await noteRepository.UpdateAsync(id, updateDto);
            return updatedNote == null ? null : MapToDto(updatedNote);
        }

        public async Task<bool> DeleteNoteAsync(int id)
        {
            return await noteRepository.DeleteAsync(id);
        }

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