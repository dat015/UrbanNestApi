using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using UrbanNest.Services.Apartment.Domain.Entities;

namespace UrbanNest.Services.Apartment.Infrastructure.Data;

public class ApartmentDbContext : DbContext
{
    public ApartmentDbContext(DbContextOptions<ApartmentDbContext> options) : base(options) { }

    // 1. Khai báo các bảng
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Block> Blocks { get; set; }
    public DbSet<Floor> Floors { get; set; }
    public DbSet<Domain.Entities.Apartment> Apartments { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<ParkingEvent> ParkingEvents { get; set; }

    // (Lưu ý: Billing, Invoice, Ticket... mình tạm comment lại để tập trung vào Apartment Service trước,
    // tránh lỗi thiếu bảng nếu bạn chưa muốn map hết 1 lúc. Nếu muốn map hết thì cứ uncomment).
    // public DbSet<Invoice> Invoices { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 2. Map tên bảng PostgreSQL (chữ thường, số nhiều)
        modelBuilder.Entity<Tenant>().ToTable("tenants");
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<Building>().ToTable("buildings");
        modelBuilder.Entity<Block>().ToTable("blocks");
        modelBuilder.Entity<Floor>().ToTable("floors");
        modelBuilder.Entity<Domain.Entities.Apartment>().ToTable("apartments");
        modelBuilder.Entity<Vehicle>().ToTable("vehicles");
        modelBuilder.Entity<ParkingEvent>().ToTable("parking_events");

        // 3. Cấu hình JSONB cho PostgreSQL
        modelBuilder.Entity<User>().Property(u => u.Metadata).HasColumnType("jsonb");
        modelBuilder.Entity<Building>().Property(b => b.Metadata).HasColumnType("jsonb");
        modelBuilder.Entity<Domain.Entities.Apartment>().Property(a => a.Metadata).HasColumnType("jsonb");
        modelBuilder.Entity<ParkingEvent>().Property(p => p.RawPayload).HasColumnType("jsonb");


        // 4. Cấu hình quan hệ (Relationships) - Quan trọng để Join bảng
        // Ví dụ: 1 Building có nhiều Blocks
        modelBuilder.Entity<Block>()
            .HasOne(b => b.Building)
            .WithMany(b => b.Blocks)
            .HasForeignKey(b => b.BuildingId)
            .OnDelete(DeleteBehavior.Cascade); // Xóa tòa nhà -> Xóa luôn block

        modelBuilder.Entity<Floor>()
            .HasOne(f => f.Block)
            .WithMany(b => b.Floors)
            .HasForeignKey(f => f.BlockId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Domain.Entities.Apartment>()
            .HasOne(a => a.Floor)
            .WithMany(f => f.Apartments)
            .HasForeignKey(a => a.FloorId)
            .OnDelete(DeleteBehavior.Cascade);

        // Quan hệ User - Tenant
        modelBuilder.Entity<User>()
            .HasOne(u => u.Tenant)
            .WithMany(t => t.Users)
            .HasForeignKey(u => u.TenantId);
    }
}