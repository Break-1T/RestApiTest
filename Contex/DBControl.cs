using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contex
{
    public static class DBControl
    {
        public static bool AddUser(User user)
        {
            try
            {
                using (ApplicationContex db = new ApplicationContex())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool DeleteUser(User user)
        {
            try
            {
                using (ApplicationContex db = new ApplicationContex())
                {
                    var result = db.Users.FirstOrDefault(x => x.Id == user.Id);
                    if (result!=null)
                        db.Users.Remove(result);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<User> GetUserList()
        {
            try
            {
                using (ApplicationContex db = new ApplicationContex())
                {
                    return db.Users.ToList();
                }
            }
            catch
            {
                return new List<User>();
            }
        }
    }
}
