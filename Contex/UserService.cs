using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contex.Infrastructure;
using Contex.Models;

namespace Contex
{
    public class UserService:IUserService
    {
        public IEnumerable<User> GetUser()
        {
            using (ApplicationContex db = new ApplicationContex())
            {
                return db.Users.ToList();
            }
        }

        public async Task<IEnumerable<User>> GetUserAsync()
        {
            return await Task.Run(()=> GetUser());
        }

        public void DeleteUser()
        {
            throw new NotImplementedException();
        }

        public void PushUser()
        {
            throw new NotImplementedException();
        }
    }
}
