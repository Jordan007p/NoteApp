using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NoteAppBackend.Models.Entities;

namespace NoteAppBackend.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Note> Notes { get; set; }

        // Applies configurations from all IEntityTypeConfiguration classes in assembly
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        // Saves changes asynchronously and sets timestamps before saving
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTimestamps();

            return await base.SaveChangesAsync(cancellationToken);
        }

        // Saves changes synchronously and sets timestamps before saving 
        public override int SaveChanges()
        {
            SetTimestamps();

            return base.SaveChanges();
        }

        // Helper method to automatically set CreatedAt and UpdatedAt timestamps
        private void SetTimestamps()
        {
            IEnumerable<EntityEntry> entries = ChangeTracker.Entries().Where(e => e is { Entity: Note, State: EntityState.Added or EntityState.Modified });

            foreach (EntityEntry entry in entries)
            {
                Note note = (Note)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    note.CreatedAt = DateTime.UtcNow;
                }

                note.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}