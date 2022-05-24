using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meum.Model;

namespace Meum.Catalog
{
   public interface ILoginCatalog
    {
         public List<User> GetAll();
        public User Get(string username);
       
        public bool Contains(User user);
        
    }
}
