namespace Customers.Persistence.Models.DataModels
{
    using System.ComponentModel.DataAnnotations;
    using Customers.Persistence.Models.DataModels.BaseClasses;
    using Newtonsoft.Json;

    public class User : EntityBase
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
