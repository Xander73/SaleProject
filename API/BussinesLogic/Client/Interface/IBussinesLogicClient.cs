using _Core.Models.Requests;
using _Core.Models.Responses;
using Core.Models.Entitys;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APILayer.BussinesLogic.Client.Interface
{
    public interface IBussinesLogicClient
    {
        Task<ActionResult<string>> GetSale(SaleRequest request, CancellationToken token);

        Task<ActionResult<IEnumerable<Product>>> GetProducts(string BaseUrl, CancellationToken token);
    }
}
