namespace FinBetApi.Infrastructure.DataAccess.SqlServer.Entities
{
    public class ClientContact
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public Client? Client { get; set; }

        public string ContractType { get; set; } = string.Empty;
        public string ContractValue { get; set; } = string.Empty;
    }
}
