namespace Vloxy.SuperaRewards.Domain.models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PointsCost { get; set; }
        public int StockQuantity { get; set; }
    }
}
