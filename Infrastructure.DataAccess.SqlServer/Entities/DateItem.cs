namespace FinBetApi.Infrastructure.DataAccess.SqlServer.Entities
{
    public class DateItem
    {
        public long Id { get; set; }
        public int ItemId { get; set; }
        public Item? Item { get; set; }
        public DateTime Date { get; set; }
    }
}
