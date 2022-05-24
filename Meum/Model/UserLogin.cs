using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
    public class UserLogin
    {
        private readonly static List<User> users = new List<User>()
        {
            new User("MeumAdmin", "Slange123!"),
            
        };

        public static List<User> Users => new List<User>(users);
    }
}
