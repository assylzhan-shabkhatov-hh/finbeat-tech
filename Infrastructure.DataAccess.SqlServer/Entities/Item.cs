namespace FinBetApi.Infrastructure.DataAccess.SqlServer.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}
