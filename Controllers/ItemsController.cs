using Catalog.Repositories;
using Catalog.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace Catalog.Controllers{
    
    [ApiController]
    [Route("items")] // /items
    public class ItemsController : ControllerBase{
        private readonly InMemoryItemsRepository repository;

        public ItemsController()
        {
            repository = new InMemoryItemsRepository();
        }

        [HttpGet] // [GET] /items
        public IEnumerable<Item> GetItems(){
            return repository.GetItems();
        }

        [HttpGet("{id}")] // [GET] /items/{id}
        public ActionResult<Item> GetItem(Guid id){
            var item = repository.GetItem(id);

            if(item is null)
                return NotFound();
            return Ok(item);
        }

    }

}