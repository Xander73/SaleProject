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
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductRepo _repository;
        private ILogger<BuyerController> _logger;


        public ProductController(ProductRepo repository, ILogger<BuyerController> logger)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "Встроен в ProductController");
        }


        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll(CancellationToken token)
        {
            _logger.LogInformation("Запущен метод ProductController.GetAll");
            return await _repository.GetAll(token);
        }


        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> Get([FromRoute]Guid productId, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод ProductController.Get");
            return await _repository.Get(productId, token);
        }

        
        [HttpPost]
        public async Task Post([FromBody] Product product, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод ProductController.Post");
            await _repository.Create(product, token);
        }


        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] Product product, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод ProductController.Put");
            await _repository.Update(id, product, token);

        }


        [HttpDelete("{id}")]
        public async Task Delete(Guid id, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод ProductController.Delete");
            await _repository.Delete(id, token);
        }
    }
}
