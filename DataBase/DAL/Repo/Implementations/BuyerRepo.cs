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
    public class BuyerRepo : IBuyerRepo
    {
        AppDbContext _db;


        public BuyerRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task Create(Buyer item, CancellationToken token)
        {
            _db.Buyers.Add(item);
            _db.SaveChanges();
        }

        public async Task Delete(Guid id, CancellationToken token)
        {
            _db.Buyers.Remove(Get(id, token).Result.Value);
            _db.SaveChanges();
        }

        public async Task<ActionResult<IEnumerable<Buyer>>> GetAll(CancellationToken token)
        {
            return await _db.Buyers.ToListAsync() ;
        }


        public async Task<ActionResult<Buyer>> Get(Guid id, CancellationToken token)
        {
            return await _db.Buyers.FirstOrDefaultAsync(b => b.Id == id);
        }


        public async Task Update(Guid id, Buyer item, CancellationToken token)
        {
            Buyer buyer = _db.Buyers.Where(b => b.Id == id).FirstOrDefault();
            if (buyer != null)
            {
                buyer.SalesIds = item.SalesIds;
                buyer.Name = item.Name;
                await _db.SaveChangesAsync();
            }
        }
    }
}
