using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Context.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int  Age { get; set; }
        public DateTime CurrentTime { get; set; }
    }
}
