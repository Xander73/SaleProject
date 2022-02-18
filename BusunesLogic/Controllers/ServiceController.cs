using _Core.Models.DTO;
using _Core.Models.Responses;
using BussunesLogic.DataLogic;
using BussunesLogic.DataLogic.Client.Implementation;
using Core.Models.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusunesLogic.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private ILogger<ServiceController> _logger;


        public ServiceController(ILogger<ServiceController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "Встроен в ServiceController");
        }


        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll(CancellationToken token)
        {
            _logger.LogInformation("Запущен метод ServiceController.GetAll");
            return await DataBaseConnect.GetAllProducts(new DataBaseLogic(new HttpClient()), token );
        }

        [HttpGet("buy")]
        public async Task<ActionResult<string>> Get([FromBody] BuyDTO buy, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод ServiceController.Get");

            string result = "";
            SalesPoint sp = new SalesPoint();
            sp.Id = Guid.NewGuid();
            sp.Name = DateTime.Now.ToString();


            Sale sale = new Sale();
            if (buy.BuyerId != Guid.Empty)
            {
                sale.BuyerId = buy.BuyerId;
            }

            foreach (var item in buy.ProductsIdQuantity)
            {
                Guid productId = item.Key;
                int quantityBuy = item.Value;

                sp.ProvidedProducts.Add(new ProvidedProduct 
                { 
                    ProductId = productId, 
                    ProductQuantity = quantityBuy 
                });

                Product product = GetProduct(productId, token).Result.Value;
                int quantityStock = GetQuantity(productId, token).Result.Value;
                int quantityAfterSale = quantityStock - quantityBuy;
                sale.Date = DateTime.Now.ToShortDateString();
                sale.Time = DateTime.Now.ToShortDateString();                
                
                if (quantityStock > quantityBuy)
                {
                    await UpdateQuantity(productId, quantityAfterSale, token);                    
                    result = $"{product.Name} проданы в количестве {quantityBuy}.\n На складе осталось {quantityAfterSale} штук.";
                    sale.SalesData.Add(new SaleData
                    {
                        ProductId = productId,
                        ProductQuantity = quantityBuy,
                        ProductIdAmount = quantityBuy * product.Price
                    });
                    sale.TotalAmount += quantityBuy * product.Price;
                }               
                else
                {
                    result = $"{product.Name} не хватает. На складе осталось {quantityStock}\n";                    
                }                
            };

            WriteSalesPointEntity(sp);
            await WriteSaleEntity(sale, token);
            return result;
        }         
        

        private async Task<ActionResult<Product>> GetProduct(Guid productId, CancellationToken token)
        {
            return await DataBaseConnect.GetProduct(productId,
                new DataBaseLogic(new HttpClient()),
                token);
        }


        private async Task<ActionResult<int>> GetQuantity(Guid productId, CancellationToken token)
        {
            return await DataBaseConnect.GetQuantity(productId,
                new DataBaseLogic(new HttpClient()), token);
        }
        


        private async Task UpdateQuantity(Guid productId, int quantityAfterSale, CancellationToken token)
        {
            await DataBaseConnect.UpdateQuantity(productId, quantityAfterSale,
                new DataBaseLogic(new HttpClient()), token);
        }


        private async Task WriteSaleEntity(Sale sale, CancellationToken token)
        {
            await DataBaseConnect.AddSaleEntity(sale, new DataBaseLogic( new HttpClient()), token);
        }


        void WriteSalesPointEntity(SalesPoint sp)
        {

        }
    }
}
