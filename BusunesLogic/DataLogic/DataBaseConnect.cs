using _Core.Models.Requests;
using BussunesLogic.DataLogic.Client.Interface;
using Core.Models.Entitys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BussunesLogic.DataLogic
{
    public class DataBaseConnect
    {
        private static string BaseUrl = "";


        public static async Task<ActionResult<Product>> GetProduct(Guid productId, 
            IDataBaseLogic _dataBaseLogic,
            CancellationToken token)
        {
            var request = new ProductRequest()
            {
                ClientBaseAddres = BaseUrl,
                ProductId = productId
            };

            return await _dataBaseLogic.GetProduct(request, token);
        }


        public static async Task<ActionResult<int>> GetQuantity(Guid productId, 
            IDataBaseLogic _dataBaseLogic,
            CancellationToken token)
        {
            var request = new ProvidedProductRequest()
            {
                ClientBaseAddres = BaseUrl,
                ProductId = productId
            };

            return await _dataBaseLogic.GetQuantity(request, token);
        }


        public static async Task UpdateQuantity(Guid productId, 
            int quantityAfterSale, 
            IDataBaseLogic _dataBaseLogic,
            CancellationToken token)
        {
            var request = new UpdateQuantityRequest()
            {
                ClientBaseAddres = BaseUrl,
                ProductId = productId,
                QuantityAfterSale = quantityAfterSale
            };

            await _dataBaseLogic.UpdateQuantity(request, token);
        }


        public static async Task AddSaleEntity(Sale sale, 
            IDataBaseLogic _dataBaseLogic,
            CancellationToken token)
        {
            var request = new SaleAddEntity
            {
                ClientBaseAddres = BaseUrl,
                SaleEntity = sale
            };

            await _dataBaseLogic.AddSaleEntity(request, token);
        }


        public static async Task<ActionResult<IEnumerable<Product>>> GetAllProducts(
            IDataBaseLogic _dataBaseLogic,
            CancellationToken token)
        {
            return await _dataBaseLogic.GetAllProducts(BaseUrl, token);
        }
    }
}
