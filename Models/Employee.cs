using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi.Models
{
    public class Employee
    {
        [Key]
        public int Id {get;set;}
        public string? Firstname {get;set;}
        public string? Surname {get;set;}
        public string? Username {get;set;}
        public string? Password {get;set;}
        public bool IsComplete {get;set;}
    }
}