using BudgetManager.Shared.DataAccess.MongoDB;
using BudgetManager.Shared.Models.MongoDB.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace BudgetManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly MongoDbContext db;

        public WalletController(MongoDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await db.Wallets.Find(_ => true).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet(Wallet wallet)
        {
            if (wallet is null)
            {
                return BadRequest();
            }

            await db.Wallets.InsertOneAsync(wallet);

            return CreatedAtAction(nameof(GetAll), wallet);
        }
    }
}
