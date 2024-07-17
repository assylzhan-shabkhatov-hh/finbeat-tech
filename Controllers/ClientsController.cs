using FinBetApi.Infrastructure.DataAccess.SqlServer.Entities;
using FinBetApi.Infrastructure.DataAccess.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinBetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly FinBetDbContext dbContext;

        public ClientsController(FinBetDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("WithContatsNumber")]
        public async Task<IEnumerable<object>> GetClientsWithContactCount(CancellationToken cancellationToken)
        {
            return await dbContext.Clients.AsNoTracking()
                .Select(x => new { x.ClientName, x.Contacts.Count })
                .ToListAsync(cancellationToken);
        }
        [HttpGet("WithMoreThan2Contacts")]
        public async Task<IEnumerable<object>> GetClientsWithMore2Contacts(CancellationToken cancellationToken)
        {
            var minContactNumbers = 2;
            return await dbContext.Clients.AsNoTracking()
                .Select(x => new { x.ClientName, x.Contacts.Count })
                .Where(x => x.Count > minContactNumbers)
                .ToListAsync(cancellationToken);
        }
    }
}
