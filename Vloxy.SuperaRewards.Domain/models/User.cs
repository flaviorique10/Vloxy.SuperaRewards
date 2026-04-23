namespace Vloxy.SuperaRewards.Domain.models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Client";
        public int TotalPoints { get; set; } = 0;
    }
}
