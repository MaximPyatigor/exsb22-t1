using API.Services.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IRepository<Transaction> repository;

        public TransactionsController(IRepository<Transaction> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Transaction>> GetAll() =>
            await repository.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> Get(Guid id)
        {
            var transaction = await repository.GetAsync(id);

            if (transaction is null)
            {
                return NotFound();
            }

            return transaction;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Transaction transaction)
        {
            await repository.InsertAsync(transaction);
            return CreatedAtAction(nameof(Get), new { id = transaction.Id }, transaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Transaction transaction)
        {
            var existingTransaction = await repository.GetAsync(id);

            if (existingTransaction is null)
            {
                return NotFound();
            }

            transaction.Id = existingTransaction.Id;
            await repository.UpdateAsync(id, transaction);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var transaction = await repository.GetAsync(id);

            if (transaction is null)
            {
                return NotFound();
            }

            await repository.RemoveAsync(id);

            return NoContent();
        }
    }
}
