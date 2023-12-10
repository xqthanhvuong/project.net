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

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Roomimg> Roomimgs { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DTXQ-CTV;Database=WEBHOTEL;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__ACCOUNT__536C85E535657CE3");

            entity.ToTable("ACCOUNT");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CUSTOMER__3214EC27CCCE7CC8");

            entity.ToTable("CUSTOMER");

            entity.Property(e => e.Id)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RESERVAT__3214EC2726D6641C");

            entity.ToTable("RESERVATION");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CheckIn).HasColumnType("date");
            entity.Property(e => e.CheckOut).HasColumnType("date");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("CustomerID");
            entity.Property(e => e.RoomId).HasColumnName("RoomID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__RESERVATI__Custo__403A8C7D");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__RESERVATI__RoomI__412EB0B6");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROOM__3214EC27E0CA7C6B");

            entity.ToTable("ROOM");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Type).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__ROOM__TypeID__3D5E1FD2");
        });

        modelBuilder.Entity<Roomimg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROOMIMG__3214EC278AA6D950");

            entity.ToTable("ROOMIMG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Link).IsUnicode(false);
            entity.Property(e => e.RoomId).HasColumnName("RoomID");

            entity.HasOne(d => d.Room).WithMany(p => p.Roomimgs)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__ROOMIMG__RoomID__440B1D61");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TYPE__3214EC279A49E363");

            entity.ToTable("TYPE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
