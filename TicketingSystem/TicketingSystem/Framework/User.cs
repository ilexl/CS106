using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Framework
{
    public class User
    {
        public enum Type : int
        {
            User = 1,
            Tech = 2,
            Admin = 3,
            Test = 4
        }
    }
}
