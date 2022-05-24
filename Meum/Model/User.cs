using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meum.Model
{
   

    public class User
    {
        public String UserName { get; set; }
        public String Password { get; set; }
       


        

        public User() 
        {
        }

      

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
            
        }

        public override string ToString()
        {
            return $"{nameof(UserName)}: {UserName}, {nameof(Password)}: {Password}";
        }

        protected bool Equals(User other)
        {
            return UserName == other.UserName && Password == other.Password;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

       //public override int GetHashCode()
       // {
       //     return HashCode.Combine(UserName, Password);
       // }
      
    }
}
