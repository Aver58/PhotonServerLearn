using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer.Model
{
    public class UserInfo
    {
        public virtual int userID { set; get; }
        public virtual string userName { set; get; }
        public virtual string password { set; get; }
        public virtual string nickName { set; get; }
        public virtual int VictoryRound { set; get; }
        public virtual int DefeatRound { set; get; }
    }
}
