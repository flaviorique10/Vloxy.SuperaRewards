namespace Vloxy.SuperaRewards.Domain.models
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public string Description { get; set; } = string.Empty; 
        public int PointsChanged { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}
