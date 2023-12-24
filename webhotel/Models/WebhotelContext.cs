using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace webhotel.Models;

public partial class WebhotelContext : DbContext
{
    public WebhotelContext()
    {
    }

    public WebhotelContext(DbContextOptions<WebhotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Forgotpass> Forgotpasses { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Roomimg> Roomimgs { get; set; }

    public virtual DbSet<Roomtype> Roomtypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DTXQ-CTV;Database=WEBHOTEL;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__ACCOUNT__536C85E592EA2E45");

            entity.ToTable("ACCOUNT");

            entity.HasIndex(e => e.Email, "UQ__ACCOUNT__A9D1053403CCEB5B").IsUnique();

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CUSTOMER__3214EC27798FB990");

            entity.ToTable("CUSTOMER");

            entity.Property(e => e.Id)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(11);
        });

        modelBuilder.Entity<Forgotpass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FORGOTPA__3214EC0735A8D5D1");

            entity.ToTable("FORGOTPASS");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ResetPass)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Forgotpasses)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__FORGOTPAS__Usern__48CFD27E");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RESERVAT__3214EC274E1C231E");

            entity.ToTable("RESERVATION");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CheckIn).HasColumnType("date");
            entity.Property(e => e.CheckOut).HasColumnType("date");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("CustomerID");
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__RESERVATI__Custo__412EB0B6");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__RESERVATI__RoomI__4222D4EF");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__RESERVATI__Usern__4316F928");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROOM__3214EC27D5F3560E");

            entity.ToTable("ROOM");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Type).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__ROOM__TypeID__3E52440B");
        });

        modelBuilder.Entity<Roomimg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROOMIMG__3214EC27E7A0EE4C");

            entity.ToTable("ROOMIMG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Link).IsUnicode(false);
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Type).WithMany(p => p.Roomimgs)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__ROOMIMG__TypeID__45F365D3");
        });

        modelBuilder.Entity<Roomtype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROOMTYPE__3214EC27066E5408");

            entity.ToTable("ROOMTYPE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Sumary).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
