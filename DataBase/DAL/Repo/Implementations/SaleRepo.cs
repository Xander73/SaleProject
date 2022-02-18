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
    public class SaleRepo : ISaleRepo
    {
        AppDbContext _db;


        public SaleRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task Create(Sale item, CancellationToken token)
        {
            _db.Sales.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id, CancellationToken token)
        {
            _db.Sales.Remove(Get(id, token).Result.Value);
            await _db.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Sale>>> GetAll(CancellationToken token)
        {
            return await _db.Sales.Include(s => s.SalesData).ToListAsync();
        }


        public async Task<ActionResult<Sale>> Get(Guid id, CancellationToken token)
        {
            return await _db.Sales.Include(s => s.SalesData).FirstOrDefaultAsync(sp => sp.Id == id);
        }

        public async Task Update(Guid id, Sale item, CancellationToken token)
        {
            Sale sale = _db.Sales.Include(s => s.SalesData).Where(b => b.Id == id).FirstOrDefault();
            if (sale != null)
            {
                sale.SalesData = item.SalesData;
                sale.TotalAmount = item.TotalAmount;
                sale.BuyerId = item.BuyerId;
                sale.Date = item.Date;
                sale.Time = item.Time;
                sale.SalesPointId = item.SalesPointId;
                await _db.SaveChangesAsync();
            }
        }
    }
}
