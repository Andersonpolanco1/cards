using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardDbContext context;

        public CardsController(CardDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            return Ok(await context.Cards.AsNoTracking().ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.Id = Guid.NewGuid();
            await context.Cards.AddAsync(card);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        [HttpGet()]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var card = await context.Cards.FindAsync(id);
            return object.Equals(card, null) ? NotFound(new { meesage = "Card not exists.", id }) : Ok(card);
        }


        [Route("{id:guid}"), HttpPut]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            Card? cardToUpdate = await context.Cards.AsNoTracking().FirstOrDefaultAsync(x => Equals(x.Id, id));

            if (Equals(cardToUpdate, null))
                return NotFound(new { Message = "Can´t find the card to update." });
            
            context.Cards.Update(card);
            await context.SaveChangesAsync();
            return Ok(card);
        }

        [Route("{id:guid}"), HttpDelete]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            Card? cardToDelete = await context.Cards.FirstOrDefaultAsync(x => Equals(x.Id, id));

            if (Equals(cardToDelete, null))
                return NotFound(new { Message = "Can´t find the card to delete." });

            context.Cards.Remove(cardToDelete);
            await context.SaveChangesAsync();

            return Ok();

        }
    }
}

