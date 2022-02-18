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
    public class SalesPointRepo : ISalesPointRepo
    {
        AppDbContext _db;


        public SalesPointRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task Create(SalesPoint item, CancellationToken token)
        {
            _db.SalesPoints.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id, CancellationToken token)
        {
            _db.SalesPoints.Remove(Get(id, token).Result.Value);
            await _db.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<SalesPoint>>> GetAll(CancellationToken token)
        {
            return await _db.SalesPoints.Include(sp => sp.ProvidedProducts).ToListAsync();
        }


        public async Task<ActionResult<SalesPoint>> Get(Guid id, CancellationToken token)
        {
            return await _db.SalesPoints.Include(sp => sp.ProvidedProducts).FirstOrDefaultAsync(sp => sp.Id == id);
        }


        public async Task<ActionResult<int>> GetQuantity(Guid productId, CancellationToken token)
        {
            return _db.ProvidedProducts.FirstOrDefaultAsync(pp => pp.Id == productId).Result.ProductQuantity;
        }


        public async Task Update(Guid id, SalesPoint item, CancellationToken token)
        {
            SalesPoint salesPoint = _db.SalesPoints.Include(sp => sp.ProvidedProducts).Where(b => b.Id == id).FirstOrDefault();
            if (salesPoint != null)
            {
                salesPoint.Name = item.Name;
                salesPoint.ProvidedProducts = item.ProvidedProducts;
                await _db.SaveChangesAsync();
            }
        }
    }
}
