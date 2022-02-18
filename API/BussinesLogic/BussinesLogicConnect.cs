


using _Core.Models.DTO;
using _Core.Models.Requests;
using _Core.Models.Responses;
using APILayer.BussinesLogic.Client.Interface;
using Core.Models.Entitys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APILayer.BussinesLogic
{
    public class BussinesLogicConnect
    {
        private static string BaseUrl { get; } = "https://localhost:44329";


        public static async Task<ActionResult<string>> GetSale(BuyDTO buy,
                                           IBussinesLogicClient bussinesLogic, 
                                           CancellationToken token)
        {
            var request = new SaleRequest()
            {
                ClientBaseAddres = BaseUrl,
                Buy = buy
            };

            return await bussinesLogic.GetSale(request);
        }


        public static async Task<ActionResult<IEnumerable<Product>>> GetProducts(
            IBussinesLogicClient bussinesLogic,
            CancellationToken token)
        {
            return await bussinesLogic.GetProducts(BaseUrl);
        }
    }
}
