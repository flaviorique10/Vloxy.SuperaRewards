namespace Vloxy.SuperaRewards.Application.Dtos
{
    public class AddPointsRequest
    {
        public int TargetUserId { get; set; } 
        public int Points { get; set; }       
        public string Reason { get; set; } = string.Empty; 
    }
}
