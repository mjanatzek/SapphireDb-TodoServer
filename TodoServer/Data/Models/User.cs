using System.ComponentModel.DataAnnotations;
using SapphireDb.Attributes;

namespace TodoServer.Data.Models
{
    public class User : Base
    {
        [Updatable]
        [Required]
        [MinLength(3)]
        public string Username { get; set; }
    }
}