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
        public string login;
        public string password;
        public string nickname;

        public User(int id, string login, string password, string nickname)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.nickname = nickname;
        }
    }
}
