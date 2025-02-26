namespace KcalIntakeTracker_KIT.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; }
        //public string Email { get; set; }
        //public int EmailVerified { get; set; }
        public double Weight { get; set; }
        public double FatPercentage { get; set; }

    }
}
