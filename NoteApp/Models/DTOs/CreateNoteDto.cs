namespace NoteApp.Models.DTOs;

public record CreateNoteDto(
    string Title,
    string Content
);