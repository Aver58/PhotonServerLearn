using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer.Model
{
    class Image_Model
    {
        public virtual int ID { set; get; }
        public virtual int 用户ID { set; get; }
        public virtual byte[] 头像 { set; get; }
    }
}
