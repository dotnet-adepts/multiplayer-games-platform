using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameApplication.Models.Games
{
    public class Player
    {
        public ClaimsPrincipal Principal;

        public Player(ClaimsPrincipal contextUser)
        {
            this.Principal = contextUser;
        }

        protected bool Equals(Player other)
        {
            return Principal.Identity.Name.Equals(other.Principal.Identity.Name);
        }

        public string GetName()
        {
            return Principal.Identity.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Player) obj);
        }

        public override int GetHashCode()
        {
            return Principal.Identity.Name.GetHashCode();
        }

        public static bool operator ==(Player obj1, Player obj2)
        {
            return obj1.Equals(obj2);
        }

        // this is second one '!='
        public static bool operator !=(Player obj1, Player obj2)
        {
            return !obj1.Equals(obj2);
        }
    }
}
