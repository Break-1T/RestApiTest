using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context.Models;

namespace Api.Models
{
    public class OperationApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public User User { get; set; }
    }
}
