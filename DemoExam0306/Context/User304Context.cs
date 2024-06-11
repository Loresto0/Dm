using System;
using System.Collections.Generic;
using DemoExam0306.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoExam0306.Context;

public partial class User304Context : DbContext
{
    public User304Context()
    {
    }

    public User304Context(DbContextOptions<User304Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TypesApplic> TypesApplics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=192.168.2.159; Database=user304; Username=user304; Password=93501");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("application_pk");

            entity.ToTable("application");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Client).HasColumnName("client");
            entity.Property(e => e.Commentispol).HasColumnName("commentispol");
            entity.Property(e => e.Dateaddapllication)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateaddapllication");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Ispol).HasColumnName("ispol");
            entity.Property(e => e.Numberapplication)
                .HasMaxLength(250)
                .HasColumnName("numberapplication");
            entity.Property(e => e.Rent)
                .HasMaxLength(250)
                .HasColumnName("rent");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.ClientNavigation).WithMany(p => p.ApplicationClientNavigations)
                .HasForeignKey(d => d.Client)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("application_users_fk");

            entity.HasOne(d => d.IspolNavigation).WithMany(p => p.ApplicationIspolNavigations)
                .HasForeignKey(d => d.Ispol)
                .HasConstraintName("5");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Applications)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("application_status_fk");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Applications)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("application_types_fk");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notification_pk");

            entity.ToTable("notification");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nameemploye).HasColumnName("nameemploye");
            entity.Property(e => e.Numberapplication).HasColumnName("numberapplication");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.NameemployeNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Nameemploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notification_users_fk");

            entity.HasOne(d => d.NumberapplicationNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Numberapplication)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notification_application_fk");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notification_status_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Idrole).HasName("roles_pk");

            entity.ToTable("roles");

            entity.Property(e => e.Idrole).HasColumnName("idrole");
            entity.Property(e => e.Namerole)
                .HasMaxLength(250)
                .HasColumnName("namerole");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Idstatus).HasName("status_pk");

            entity.ToTable("status");

            entity.Property(e => e.Idstatus).HasColumnName("idstatus");
            entity.Property(e => e.Statusname)
                .HasMaxLength(250)
                .HasColumnName("statusname");
        });

        modelBuilder.Entity<TypesApplic>(entity =>
        {
            entity.HasKey(e => e.Idtype).HasName("types_pk");

            entity.ToTable("TypesApplic");

            entity.Property(e => e.Idtype).HasColumnName("idtype");
            entity.Property(e => e.Nametype)
                .HasMaxLength(250)
                .HasColumnName("nametype");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("users_pk");

            entity.ToTable("users");

            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Idrole).HasColumnName("idrole");
            entity.Property(e => e.Login)
                .HasMaxLength(250)
                .HasColumnName("login");
            entity.Property(e => e.Nameuser)
                .HasMaxLength(250)
                .HasColumnName("nameuser");
            entity.Property(e => e.Password).HasMaxLength(250);

            entity.HasOne(d => d.IdroleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Idrole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_roles_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
