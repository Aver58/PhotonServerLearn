using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class UserInfo
{
    public virtual int Id { get; set; }
    public virtual string Username { get; set; }
    public virtual string Password { get; set; }
    public virtual DateTime Registerdate { get; set; }
    //public virtual string nickName { set; get; }
    //public virtual int VictoryRound { set; get; }
    //public virtual int DefeatRound { set; get; }
}
