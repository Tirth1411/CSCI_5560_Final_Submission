using Microsoft.EntityFrameworkCore;
using ROS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROS.Implement
{
    public class HW6DbContext : DbContext
    {
        public HW6DbContext(DbContextOptions<HW6DbContext> options) : base(options)
        { }

        #region Tables
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Part> Part { get; set; }
        public DbSet<Shipment> Shipment { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipment>()
                .HasKey(s => new { s.Sno, s.Pno });

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Supplier)
                .WithMany(s => s.Shipments)
                .HasForeignKey(s => s.Sno);

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Part)
                .WithMany(p => p.Shipments)
                .HasForeignKey(s => s.Pno);
        }
    }
}
