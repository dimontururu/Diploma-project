using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace task_service.Model;

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

    public virtual DbSet<ToDoList> ToDoLists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("host=localhost; port=5432; database=to-do list; username=postgres; password=admin");

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

            entity.ToTable("User");

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
