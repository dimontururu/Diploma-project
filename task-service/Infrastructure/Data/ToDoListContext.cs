using Microsoft.EntityFrameworkCore;
using task_service.Domain.Entities;

namespace task_service.Infrastructure.Data;

public partial class ToDoListContext : DbContext
{
    public ToDoListContext()
    {
    }

    public ToDoListContext(DbContextOptions<ToDoListContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Award> Awards { get; set; }

    public virtual DbSet<Case> Cases { get; set; }

    public virtual DbSet<ClientType> ClientTypes { get; set; }

    public virtual DbSet<IdClient> IdClients { get; set; }

    public virtual DbSet<ToDoList> ToDoLists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Award>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("awards_pkey");

            entity.ToTable("awards");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Case>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("case_pkey");

            entity.ToTable("case");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.DateEnd).HasColumnName("date_end");
            entity.Property(e => e.DateOfCreation).HasColumnName("date_of_creation");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValue(false)
                .HasColumnName("status");
        });

        modelBuilder.Entity<ClientType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_type_pkey");

            entity.ToTable("client_type");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
        });

        modelBuilder.Entity<IdClient>(entity =>
        {
            entity.HasKey(e => new { e.IdClient1, e.IdClientType }).HasName("id_client_pkey");

            entity.ToTable("id_client");

            entity.Property(e => e.IdClient1)
                .HasMaxLength(20)
                .HasColumnName("id_client");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IdClientType).HasColumnName("id_client_type");

            entity.HasOne(d => d.IdClientTypeNavigation).WithMany(p => p.IdClients)
                .HasForeignKey(d => d.IdClientType)
                .HasConstraintName("id_client_id_client_type_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.IdClients)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_client_id_user_fkey");
        });

        modelBuilder.Entity<ToDoList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("to-do_list_pkey");

            entity.ToTable("to-do_list");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.IdTask).HasColumnName("id_task");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.ToDoLists)
                .HasForeignKey(d => d.IdTask)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("to-do_list_id_task_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ToDoLists)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("to-do_list_id_user_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("user");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");

            entity.HasMany(d => d.IdAwards).WithMany(p => p.IdUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserAward",
                    r => r.HasOne<Award>().WithMany()
                        .HasForeignKey("IdAwards")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_awards_id_awards_fkey"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_awards_id_user_fkey"),
                    j =>
                    {
                        j.HasKey("IdUser", "IdAwards").HasName("user_awards_pkey");
                        j.ToTable("user_awards");
                        j.IndexerProperty<Guid>("IdUser").HasColumnName("id_user");
                        j.IndexerProperty<Guid>("IdAwards").HasColumnName("id_awards");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
