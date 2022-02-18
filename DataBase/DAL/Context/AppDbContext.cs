using Core.Models;
using Core.Models.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataBase.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesPoint> SalesPoints { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ProvidedProduct> ProvidedProducts { get; set; }
        public DbSet<SaleData> SalesData { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Product product1 = new Product { Id = Guid.NewGuid(), Name = "PA", Price = 1.1 };
            Product product2 = new Product { Id = Guid.NewGuid(), Name = "PB", Price = 2.2 };
            Product product3 = new Product { Id = Guid.NewGuid(), Name = "PC", Price = 3.3 };
            modelBuilder.Entity<Product>().HasData(product1, product2, product3);


            ProvidedProduct providedProduct1 = new ProvidedProduct { Id = Guid.NewGuid(), ProductId = product1.Id, ProductQuantity = 100 };
            ProvidedProduct providedProduct2 = new ProvidedProduct { Id = Guid.NewGuid(), ProductId = product2.Id, ProductQuantity = 200 };
            ProvidedProduct providedProduct3 = new ProvidedProduct { Id = Guid.NewGuid(), ProductId = product3.Id, ProductQuantity = 300 };
            modelBuilder.Entity<ProvidedProduct>().HasData(providedProduct1, providedProduct2, providedProduct3);


            SalesPoint salesPoint1 = new SalesPoint { Id = Guid.NewGuid(), Name = "SPA", ProvidedProducts = new List<ProvidedProduct> { providedProduct1 } };
            SalesPoint salesPoint2 = new SalesPoint { Id = Guid.NewGuid(), Name = "SPB", ProvidedProducts = new List<ProvidedProduct> { providedProduct2 } };
            SalesPoint salesPoint3 = new SalesPoint { Id = Guid.NewGuid(), Name = "SPC", ProvidedProducts = new List<ProvidedProduct> { providedProduct3 } };
            modelBuilder.Entity<SalesPoint>().HasData(salesPoint1, salesPoint2, salesPoint3);


            SaleData saleData1 = new SaleData { ProductId = product1.Id, ProductQuantity = 10, ProductIdAmount = product1.Price * 1 };
            SaleData saleData2 = new SaleData { ProductId = product2.Id, ProductQuantity = 20, ProductIdAmount = product1.Price * 2 };
            SaleData saleData3 = new SaleData { ProductId = product3.Id, ProductQuantity = 30, ProductIdAmount = product1.Price * 3 };
            modelBuilder.Entity<SaleData>().HasData(saleData1, saleData2, saleData3);


            Guid[] buyerIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            string date = new DateTime(2000, 1, 1).ToShortDateString();
            string time = new DateTime(2000, 1, 1, 1, 1, 1).ToShortTimeString();

            Sale sale1 = new Sale
            {
                Id = Guid.NewGuid(),
                Date = date,
                Time = time,
                SalesPointId = salesPoint1.Id,
                BuyerId = buyerIds[0],
                SalesData = new List<SaleData> { saleData1 },
                TotalAmount = 1000
            };

            Sale sale2 = new Sale
            {
                Id = Guid.NewGuid(),
                Date = date,
                Time = time,
                SalesPointId = salesPoint2.Id,
                BuyerId = buyerIds[1],
                SalesData = new List<SaleData> { saleData2 },
                TotalAmount = 2000
            };

            Sale sale3 = new Sale
            {
                Id = Guid.NewGuid(),
                Date = date,
                Time = time,
                SalesPointId = salesPoint3.Id,
                BuyerId = buyerIds[2],
                SalesData = new List<SaleData> { saleData3 },
                TotalAmount = 3000
            };

            modelBuilder.Entity<Sale>().HasData(sale1, sale2, sale3);


            modelBuilder.Entity<Buyer>().HasData
                (
                new Buyer { Id = buyerIds[0], Name = "BA", SalesIds = new List<Guid> { sale1.Id } },
                new Buyer { Id = buyerIds[1], Name = "BB", SalesIds = new List<Guid> { sale2.Id } },
                new Buyer { Id = buyerIds[2], Name = "BC", SalesIds = new List<Guid> { sale3.Id } }
                );
        }
    }
}
