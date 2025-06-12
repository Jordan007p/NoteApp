using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteAppBackend.Data;
using NoteAppBackend.Models.Entities;

namespace NoteAppBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController(AppDbContext context) : ControllerBase
{
    // GET: api/notes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
    {
        var notes = await context.Notes
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

        return Ok(notes);
    }

    // GET: api/notes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Note>> GetNote(int id)
    {
        var note = await context.Notes.FindAsync(id);

        if (note == null)
        {
            return NotFound();
        }

        return Ok(note);
    }

    // POST: api/notes
    [HttpPost]
    public async Task<ActionResult<Note>> CreateNote(CreateNoteRequest request)
    {
        var note = new Note
        {
            Title = request.Title,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        context.Notes.Add(note);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
    }

    // PUT: api/notes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(int id, CreateNoteRequest request)
    {
        var note = await context.Notes.FindAsync(id);
        if (note == null)
        {
            return NotFound();
        }

        note.Title = request.Title;
        note.Content = request.Content;
        note.UpdatedAt = DateTime.UtcNow; // Update timestamp

        await context.SaveChangesAsync();
        return NoContent();
    }
}

// Simple request model for creating notes
public class CreateNoteRequest
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}