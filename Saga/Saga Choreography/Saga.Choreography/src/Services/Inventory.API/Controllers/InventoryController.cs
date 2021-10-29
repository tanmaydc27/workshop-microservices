using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Event;
using EventBus.RabbitMQ;
using Inventory.API.Model;
using Inventory.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;


        public InventoryController(IInventoryRepository inventoryRepository)
        {
            this._inventoryRepository = inventoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Product>> Get(int productID)
        {
            var response=await _inventoryRepository.Get(productID);
            return Ok(response);
        }

    }
}
