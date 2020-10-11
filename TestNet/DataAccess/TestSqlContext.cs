using Microsoft.EntityFrameworkCore;

namespace TestNet.Entities
{
    public partial class TestSqlContext : DbContext
    {
        public TestSqlContext()
        {
        }

        public TestSqlContext(DbContextOptions<TestSqlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FileData> FileData { get; set; }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileData>(entity =>
            {
                entity.Property(e => e.ArchiveType).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.UploadDate).HasColumnType("datetime");

                entity.Property(e => e.Status).IsRequired().HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
