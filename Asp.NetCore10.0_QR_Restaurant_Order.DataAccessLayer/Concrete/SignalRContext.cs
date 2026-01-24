using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Concrete
{
    public class SignalRContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=QRRestaurantOrderDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<About> Abouts { get; set; } // Hakkımızda tablosu
        public DbSet<Booking> Bookings { get; set; } // Rezervasyon tablosu
        public DbSet<Category> Categories { get; set; } // Kategoriler Tablosu
        public DbSet<Contact> Contacts { get; set; } // İletişim tablosu
        public DbSet<Discount> Discounts { get; set; } // İndirim tablosu
        public DbSet<Feature> Features { get; set; } // Özellikler tablosu
        public DbSet<Footer> Footers { get; set; } // Footer tablosu
        public DbSet<Product> Products { get; set; } // Ürünler tablosu
        public DbSet<Testimonial> Testimonials { get; set; } // Referanslar tablosu
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Table> Tables { get; set; }


    }
}
