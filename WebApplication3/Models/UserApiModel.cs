using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Context.Models;

namespace Api.Models
{
    public class UserApiModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the user's name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the user's surname.")]
        public string Surname { get; set; }

        [IntegerValidator(MaxValue = 120, MinValue = 10)]
        public int Age { get; set; }
        
        public DateTime CurrentTime { get; set; }
        public List<Operation> Operations { get; set; }
    }
}
