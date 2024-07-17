using FinBetApi.Infrastructure.DataAccess.SqlServer;
using FinBetApi.Infrastructure.DataAccess.SqlServer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinBetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateItemsController : ControllerBase
    {
        private readonly FinBetDbContext dbContext;

        public DateItemsController(FinBetDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<object> GetIntervals(CancellationToken cancellationToken)
        {
            var response =  await dbContext.DateItems.AsNoTracking()
                .OrderBy(x => x.ItemId)
                    .ThenBy(x => x.Date)
                .ToListAsync(cancellationToken);

            return response.GroupBy(x => x.ItemId)
                .SelectMany(x => x.Zip(x.Skip(1), (first, second) => new
                {
                    Id = x.Key,
                    StartDate = first.Date,
                    EndDate = second.Date
                }));

        }
    }
}
