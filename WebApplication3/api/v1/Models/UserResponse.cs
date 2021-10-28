using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Context.Models;

namespace Api.api.v1.Models
{
    public class UserResponse : UserRequest
    {
        public int Id { get; set; }
    }
}
