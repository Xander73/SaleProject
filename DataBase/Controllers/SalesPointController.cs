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
    [Route("api/salespoint")]
    [ApiController]
    public class SalesPointController : ControllerBase
    {
        private SalesPointRepo _repository;
        private ILogger<BuyerController> _logger;


        public SalesPointController(SalesPointRepo repository, ILogger<BuyerController> logger)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "Встроен в SalesPointController");
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesPoint>>> GetAll(CancellationToken token)
        {
            _logger.LogInformation("Запущен метод SalesPointController.GetAll");
            return await _repository.GetAll(token);
        }


        [HttpGet("{productId}")]
        public async Task<ActionResult<SalesPoint>> Get([FromRoute] Guid spId, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод SalesPointController.Get");
            return await _repository.Get(spId, token);

        }


        [HttpGet("getquantityproduct/{productId}")]
        public async Task<ActionResult<int>> GetQuantityProduct([FromRoute] Guid productId, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод SalesPointController.Get");
            return await _repository.GetQuantity(productId, token);

        }


        [HttpPost]
        public async Task Post([FromBody] SalesPoint sp, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод SalesPointController.Post");
            await _repository.Create(sp, token);
        }


        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] SalesPoint sp, CancellationToken token)
        {
            await _repository.Update(id, sp, token);

        }


        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод SalesPointController.Delete");
            await _repository.Delete(id, token);
        }
    }
}
