namespace FinBetApi.Infrastructure.DataAccess.SqlServer.Entities
{
    public class Client
    {
        public long Id { get; set; }
        public string ClientName { get; set; } = string.Empty;

        public ICollection<ClientContact> Contacts { get; set; } = new List<ClientContact>();
    }
}
