namespace api.data
{
    public class User
    {
        public Guid newId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;  
        public float PositionX { get; set; } = 50;
        public float PositionY { get; set; } = 100;
        //public string Password { get; set; }
        //public string Role { get; set; }
        //public string Token { get; set; }
        //public string RefreshToken { get; set; }
        //public DateTime RefreshTokenExpiryTime { get; set; }    
    }
}