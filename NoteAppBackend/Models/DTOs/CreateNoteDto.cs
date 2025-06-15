namespace NoteAppBackend.Models.DTOs
{
    public record CreateNoteDto(
        string Title,
        string Content
    );
}