namespace KcalIntakeTracker_KIT.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public required string Username { get; set; } = string.Empty;
        public required string PasswordHash { get; set; }
        public required string Email { get; set; }
        public double Weight { get; set; }
        public double FatPercentage { get; set; }

    }
}
