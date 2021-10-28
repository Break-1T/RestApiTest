using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Api.api.v1.Models
{
    public class UserRequest
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "Please enter the userResponse's name.")]
        public string Name { get; set; }

        /// <summary>Gets or sets the surname.</summary>
        /// <value>The surname.</value>
        [Required(ErrorMessage = "Please enter the userResponse's surname.")]
        public string Surname { get; set; }

        /// <summary>Gets or sets the age.</summary>
        /// <value>The age.</value>
        [IntegerValidator(MaxValue = 100, MinValue = 10, ExcludeRange = true)]
        public int Age { get; set; }

        public DateTime CurrentTime { get; set; }
        public List<Operation> Operations { get; set; }
    }
}
