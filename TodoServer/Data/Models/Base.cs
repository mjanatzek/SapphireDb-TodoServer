using System;
using System.ComponentModel.DataAnnotations;

namespace TodoServer.Data.Models
{
    public class Base
    {
        public Base()
        {
            Id = Guid.NewGuid();
        }
        
        [Key]
        public Guid Id { get; set; }
    }
}