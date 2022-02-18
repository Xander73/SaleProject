using Core.Models.Entitys;
using DataBase.DAL.Repo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataLogic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private IBuyerRepo _buyerRepo;
        private ILogger<BuyerController> _logger;


        public BuyerController(IBuyerRepo buyerRepo, ILogger<BuyerController> logger)
        {
            _buyerRepo = buyerRepo;
            _logger = logger;
            _logger.LogDebug(1, "Встроен в BuyerController");
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buyer>>> GetAll(CancellationToken token)
        {
            _logger.LogInformation("Запущен метод BuyerController.GetAll");
            return Ok(_buyerRepo.GetAll(token));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Buyer>> Get(Guid id, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод BuyerController.Get");
            return await _buyerRepo.Get(id, token);
        }


        [HttpPost]
        public async Task Post([FromBody] Buyer buyer, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод BuyerController.Post");
            await _buyerRepo.Create(buyer, token);
        }


        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] Buyer buyer, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод BuyerController.Put");
            await _buyerRepo.Update(id, buyer, token);

        }


        [HttpDelete("{id}")]
        public async Task Delete(Guid buyer, CancellationToken token)
        {
            _logger.LogInformation("Запущен метод BuyerController.Delete");
            await _buyerRepo.Delete(buyer, token);
        }
    }
}
