using Core.Models;
using Core.Models.Entitys;
using DataBase.DAL.Context;
using DataBase.DAL.Repo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase.DAL.Repo.Implementations
{
    public class ProductRepo : IProductRepo
    {
        AppDbContext _db;


        public ProductRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task Create(Product item, CancellationToken token)
        {
            _db.Products.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id, CancellationToken token)
        {
            _db.Products.Remove(Get(id, token).Result.Value);
            await _db.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAll(CancellationToken token)
        {
            return await _db.Products.ToListAsync();
        }


        public async Task<ActionResult<Product>> Get(Guid id, CancellationToken token)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Update(Guid id, Product item, CancellationToken token)
        {
            Product product = _db.Products.Where(b => b.Id == id).FirstOrDefault();
            if (product != null)
            {
                product.Name = item.Name;
                product.Price = item.Price;
                await _db.SaveChangesAsync();
            }
        }
    }
}
