namespace NoteAppBackend.Models.DTOs
{
    public record ListItemNote(
        int Id,
        string Title,
        DateTime CreatedAt
    );
}