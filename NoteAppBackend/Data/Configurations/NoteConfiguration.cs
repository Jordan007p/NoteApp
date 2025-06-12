using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteAppBackend.Models.Entities;

namespace NoteAppBackend.Data.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        // Primary Key
        builder.HasKey(n => n.Id);

        // Properties
        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(n => n.Content)
            .HasColumnType("text");

        builder.Property(n => n.CreatedAt)
            .IsRequired();

        builder.Property(n => n.UpdatedAt)
            .IsRequired();

        // Indexes
        builder.HasIndex(n => n.CreatedAt)
            .HasDatabaseName("ix_notes_created_at");

        builder.HasIndex(n => n.Title)
            .HasDatabaseName("ix_notes_title");

        builder.ToTable("notes");
    }
}