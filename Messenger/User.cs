using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    public class User
    {
        public int id;
        public string nickname;

        public User(int id, string nickname)
        {
            this.id = id;
            this.nickname = nickname;
        }
    }
}
