using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meum.Model;

namespace Meum.Catalog
{
    public class LoginCatalog:ILoginCatalog
    {

      
            private readonly List<User> _users;

            public LoginCatalog()
            {
                _users = UserLogin.Users;
            }

            public List<User> GetAll()
            {
                return new List<User>(_users);
            }

            public User Get(string username)
            {
                User user = _users.Find(u => u.UserName == username);

                return (user != null) ? user : throw new KeyNotFoundException();
            }

      

            public bool Contains(User user)
            {
                return _users.Contains(user);
            }

           
        
    }
}
