using AwesomeDevEvents.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.Api.Persistence;

public class DevEventsDbContext : DbContext
{
    public DbSet<DevEvent> DevEvents { get; set; }
    public DbSet<DevEventSpeaker> DevEventsSpeakers { get; set; }

    public DevEventsDbContext(DbContextOptions<DevEventsDbContext> options) : base(options) { }

    // config ef do create the table
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DevEvent>(e =>
        {
            // setup id
            e.HasKey(de => de.Id);
            // setup property name as id intead Id
            e.Property(de => de.Id)
                .HasColumnName("id");

            // setup property Title as nullable
            e.Property(de => de.Title)
                .IsRequired(false)
                .HasColumnName("title");
            // setup property Descriptio with max length as 200 and as varchar(200)
            e.Property(de => de.Description)
                .HasMaxLength(200)
                .HasColumnType("varchar(200)")
                .HasColumnName("description");
            e.Property(de => de.StartDate)
                .HasColumnName("starts_date");
            e.Property(de => de.EndDate)
                .HasColumnName("end_date");

            e.HasMany(de => de.Speakers)
                .WithOne()
                .HasForeignKey(s => s.DevEventId); // setup foreign key
        });

        modelBuilder.Entity<DevEventSpeaker>(e =>
        {
            e.HasKey(de => de.Id);
            e.Property(de => de.Id)
                .HasColumnName("dev_event_speaker_id");

            e.Property(de => de.TalkTitle)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("varchar(200)")
                .HasColumnName("task_title");
            e.Property(de => de.Name)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .HasColumnName("name");
            e.Property(de => de.TaskDescription)
                .IsRequired(false)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .HasColumnName("talk_description");
            e.Property(de => de.LinkedInProfile)
                .IsRequired(false)
                .HasColumnType("varchar(70)")
                .HasMaxLength(70)
                .HasColumnName("linked_id_profile");
        });
    }
}
