using _Core.Models.Requests;
using Core.Models.Entitys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BussunesLogic.DataLogic.Client.Interface
{
    public interface IDataBaseLogic
    {
        Task<ActionResult<Product>> GetProduct(ProductRequest request, CancellationToken token);


        Task<ActionResult<int>> GetQuantity(ProvidedProductRequest request, CancellationToken token);


        Task UpdateQuantity(UpdateQuantityRequest request, CancellationToken token);


        Task AddSaleEntity(SaleAddEntity request, CancellationToken token);


        Task<ActionResult<IEnumerable<Product>>> GetAllProducts(string baseUrl, CancellationToken token);
    }
}
