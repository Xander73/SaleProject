using Core.Models.Entitys;
using DataBase.DAL.Repo.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataLogic.Controllers
{
    [Route("api/sale")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private SaleRepo _repository;
        private ILogger<BuyerController> _logger;


        public SaleController(SaleRepo repository, ILogger<BuyerController> logger)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "Встроен в SaleController");

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetAll(CancellationToken token)
        {
            _logger.LogInformation("Запущен метод SaleController.GetAll");
            return await _repository.GetAll(token);
        }


        [HttpGet("{productId}")]
        public async Task<ActionResult<Sale>> Get([FromRoute] Guid saleId, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод SaleController.Get");
            return await _repository.Get(saleId, token);
        }


        [HttpPost]
        public async Task Post([FromBody] Sale sale, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод SaleController.Post");
            await _repository.Create(sale, token);
        }


        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] Sale sale, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод SaleController.Put");
            await _repository.Update(id, sale, token);

        }


        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод SaleController.Delete");
            await _repository.Delete(id, token);
        }
    }
}
