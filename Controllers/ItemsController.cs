using FinBetApi.Infrastructure.DataAccess.SqlServer;
using FinBetApi.Infrastructure.DataAccess.SqlServer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FinBetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly FinBetDbContext dbContext;

        public ItemsController(FinBetDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Adds passed items. Delete previous items
        /// </summary>
        /// <param name="items"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddItems(KeyValuePair<string, string>[] items, CancellationToken cancellationToken)
        {
            //If there a lot of data => run "TRUNCATE TABLE Items"
            var oldItems = await dbContext.Items.ToListAsync(cancellationToken);
            dbContext.Items.RemoveRange(oldItems);

            var newItems = items.Select(x => new Item()
                {
                    Code = int.Parse(x.Key),
                    Value = x.Value,
                })
                .OrderBy(x => x.Code)
                .ToList();
            dbContext.Items.AddRange(newItems);
            
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        [HttpGet]
        public async Task<IEnumerable<Item>> GetItems(int? code, string? value, CancellationToken cancellationToken)
        {
            var query = dbContext.Items.AsNoTracking();

            if(code is not null)
            {
                query = query.Where(x => x.Code == code);
            }

            if(value is not null)
            {
                query = query.Where(x => x.Value.Contains(value));
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
