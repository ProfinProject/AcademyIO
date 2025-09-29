using AcademyIO.Core.Data;
using AcademyIO.Core.DomainObjects;
using AcademyIO.Courses.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AcademyIO.Core.Messages;
using AcademyIO.Core.Extensions;

namespace AcademyIO.Courses.API.Data
{
    public class CoursesContext(DbContextOptions<CoursesContext> options,
                                    IMediator mediator) : DbContext(options), IUnitOfWork
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<ProgressLesson> ProgressLessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new ProgressLessonsConfiguration());
            modelBuilder.Ignore<Event>();
        }

        public async Task<bool> Commit()
        {
            var success = await SaveChangesAsync() > 0;

            if (success)
                await mediator.PublishDomainEvents(this);

            return success;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entityEntry in ChangeTracker.Entries<Entity>())
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }
                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property("CreatedDate").IsModified = false;
                }
                //if (entityEntry.State == EntityState.Deleted)
                //{
                //    entityEntry.State = EntityState.Modified;
                //    entityEntry.Property("CreatedDate").IsModified = false;
                //}
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(a => a.Id);
            builder.ToTable("Courses");

            builder.Property(a => a.Name)
           .HasMaxLength(250);

            builder.Property(a => a.Description)
           .HasMaxLength(250);
        }
    }

    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(a => a.Id);
            builder.ToTable("Lessons");

            builder.Property(a => a.Name)
            .HasMaxLength(250);

            builder.Property(a => a.Subject)
           .HasMaxLength(500);
        }
    }

    public class ProgressLessonsConfiguration : IEntityTypeConfiguration<ProgressLesson>
    {
        public void Configure(EntityTypeBuilder<ProgressLesson> builder)
        {
            builder.HasKey(a => a.Id);
            builder.ToTable("ProgressLessons");
        }
    }
}
