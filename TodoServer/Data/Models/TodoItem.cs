using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SapphireDb.Attributes;
using SapphireDb.Models;

namespace TodoServer.Data.Models
{
    [CreateEvent(nameof(OnCreate))]
    [CreateAuth]
    [UpdateAuth(functionName: nameof(CanUpdate))]
    public class TodoItem : Base
    {
        [Required]
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public Guid AssignedToId { get; set; }
        
        public DateTime DueDate { get; set; }

        [Updatable]
        [Range(0, 100)]
        public int Progress { get; set; }
        
        public Guid OwnerId { get; set; }

        private void OnCreate(HttpInformation httpInformation)
        {
            if (httpInformation.User.Identity.IsAuthenticated)
            {
                Guid id = Guid.Parse(httpInformation.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value ?? "");
                OwnerId = id;
            }
        }
        
        private bool CanUpdate(HttpInformation httpInformation)
        {
            if (httpInformation.User.Identity.IsAuthenticated)
            {
                Guid id = Guid.Parse(httpInformation.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value ?? "");

                return AssignedToId == id;
            }

            return false;
        }
    }
}