namespace NoteAppBackend.Models.DTOs;

public record NoteDto(
    int Id,
    string Title,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt
);