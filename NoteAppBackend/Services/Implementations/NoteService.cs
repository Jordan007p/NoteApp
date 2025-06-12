using NoteAppBackend.Models.DTOs;
using NoteAppBackend.Models.Entities;
using NoteAppBackend.Repositories.Interfaces;
using NoteAppBackend.Services.Interfaces;

namespace NoteAppBackend.Services.Implementations;

public class NoteService(INoteRepository noteRepository, ILogger<NoteService> logger) : INoteService
{
    public async Task<NoteDto?> GetNoteByIdAsync(int id)
    {
        logger.LogInformation("Getting note with ID: {Id}", id);

        var note = await noteRepository.GetByIdAsync(id);
        return note == null ? null : MapToDto(note);
    }

    public async Task<IEnumerable<NoteDto>> GetAllNotesAsync()
    {
        logger.LogInformation("Getting all notes");

        var notes = await noteRepository.GetAllAsync();
        return notes.Select(MapToDto);
    }

    public async Task<NoteDto> CreateNoteAsync(CreateNoteDto createDto)
    {
        logger.LogInformation("Creating new note with title: {Title}", createDto.Title);

        var note = new Note
        {
            Title = createDto.Title,
            Content = createDto.Content
        };

        var createdNote = await noteRepository.CreateAsync(note);
        return MapToDto(createdNote);
    }

    public async Task<NoteDto?> UpdateNoteAsync(int id, UpdateNoteDto updateDto)
    {
        logger.LogInformation("Updating note with ID: {Id}", id);

        var existingNote = await noteRepository.GetByIdAsync(id);
        if (existingNote == null) return null;

        existingNote.Title = updateDto.Title;
        existingNote.Content = updateDto.Content;

        var updatedNote = await noteRepository.UpdateAsync(existingNote);
        return MapToDto(updatedNote);
    }

    public async Task<bool> DeleteNoteAsync(int id)
    {
        logger.LogInformation("Deleting note with ID: {Id}", id);

        return await noteRepository.DeleteAsync(id);
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