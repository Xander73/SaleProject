/*Описание принципа работы программы. 
 * "APILayer" в контроллере принимает данные о покупке 
 * и передает их в слой обработки данных "BussunesLogic".
 * В слое обработки данных производятся запросы в базу данных, 
 * в "APILayer" высылается ответ с результатами обработки, 
 * а "DataLogic" отправляется объект с запросом на сохранение его в базе данных. 
 * 
 * !!! Проект не завершен из-за нехватки времени. 
 * Не закончено объединение моделей. База данных не запущена. Ввиду этого написать юнит тесты невозможно.
 * 
 * Что сделано.
 * 1) Разрабоны модели данных основных сущностей.
 * 2) Разрабоно web API с реализацией CRUD операций с базой данных над всеми моделями описанными в п.1
 * 3) Разработано web API с реализацией бизнес-логики.
 * 4) Реализован логгер.
 * 5) Методы контроллеров, запросов, обращений в БД реализованы асинхронно.
 */

using _Core.Models.DTO;
using _Core.Models.Responses;
using APILayer.BussinesLogic;
using APILayer.BussinesLogic.Client.Implementation;
using APILayer.BussinesLogic.Client.Interface;
using Core.Models.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;



namespace APILayer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private ILogger<SaleController> _logger;

        public SaleController(ILogger<SaleController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "Встроен в SaleController");
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get(CancellationToken token)
        {
            _logger.LogInformation("Запущен метод SaleController.Get");
            var response = await BussinesLogicConnect.GetProducts(
               new BussinesLogicClient(new HttpClient()), 
               token
               );
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] BuyDTO buy, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод SaleController.Post");
            var response = await BussinesLogicConnect.GetSale(
                buy,
                new BussinesLogicClient(new HttpClient()),
                token
                );

            return Ok(response);

        }
    }
}
