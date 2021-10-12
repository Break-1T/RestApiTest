using System;
using System.ComponentModel.DataAnnotations;

namespace Contex.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int  Age { get; set; }
        public DateTime CurrentTime { get; set; }
    }
}
