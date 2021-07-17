using Catalog.Repositories;
using Catalog.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Catalog.Dtos;

namespace Catalog.Controllers{
    
    [ApiController]
    [Route("items")] // /items
    public class ItemsController : ControllerBase{
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet] // [GET] /items
        public IEnumerable<ItemDto> GetItems(){
            return repository.GetItems().Select(item => item.AsDto());
        }

        [HttpGet("{id}")] // [GET] /items/{id}
        public ActionResult<ItemDto> GetItem(Guid id){
            var item = repository.GetItem(id);

            if(item is null)
                return NotFound();
            
            return Ok(item.AsDto());
        }

        [HttpPost] // [POST] /items
        public ActionResult<ItemDto> CreateItem(CreateItemDto createItemDto){

            Item item = new(){
                Id = Guid.NewGuid(),
                Name = createItemDto.Name,
                Price= createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new {id = item.Id}, item.AsDto());
        }

        [HttpPut] // [PUT] /items/{id}
        [Route("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto){
            var target = repository.GetItem(id);

            if (target is null)
                return NotFound();
            
            Item updatedItem = target with {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            repository.UpdateItem(updatedItem);
            return NoContent();
        }

        [HttpDelete]    // [DELETE] /items{id}
        [Route("{id}")]
        public ActionResult DeleteItem(Guid id){
            var victim = repository.GetItem(id);

            if (victim is null)
                return NotFound();
            
            repository.DeleteItem(id);
            return NoContent();
        }
    }
}