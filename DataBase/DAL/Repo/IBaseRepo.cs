using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase.DAL.Repo
{
    public interface IBaseRepo<T> where T : class
    {
        Task Create(T item, CancellationToken token);


        Task Update(Guid id, T item, CancellationToken token);


        Task Delete(Guid id, CancellationToken token);


        Task<ActionResult<IEnumerable<T>>> GetAll(CancellationToken token);


        Task<ActionResult<T>> Get(Guid id, CancellationToken token);
    }
}
