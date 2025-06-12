namespace NoteAppBackend.Models.DTOs;

public record UpdateNoteDto(
    string Title,
    string Content
);