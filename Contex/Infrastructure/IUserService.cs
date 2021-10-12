using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contex.Models;

namespace Contex.Infrastructure
{
    public interface IUserService
    {
        public IEnumerable<User> GetUser();
        public void DeleteUser();
        public void PushUser();
    }
}
