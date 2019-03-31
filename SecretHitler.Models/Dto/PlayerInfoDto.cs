using System;
namespace SecretHitler.Models.Dto
{
    public class PlayerInfoDto
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public RoleType? Role { get; set; }
    }
}
