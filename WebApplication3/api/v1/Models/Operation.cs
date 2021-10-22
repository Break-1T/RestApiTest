using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context.Models;

namespace Api.api.v1.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public User User { get; set; }
    }
}
