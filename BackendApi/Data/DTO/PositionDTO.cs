using Microsoft.AspNetCore.Mvc;

namespace api.data
{
    public class PositionDTO
    {
        public float posX { get; set; }
        public float posY { get; set; }
    }
    public class NewUserDTO
    {
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
    }
}
