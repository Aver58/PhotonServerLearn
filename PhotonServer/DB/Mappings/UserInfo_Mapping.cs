//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using GameServer.Model;

//namespace MyServer.DB.DBMapping
//{
//    class UserInfo_Mapping:ClassMap<UserInfo_Model>//这里代表要映射的类的名字
//    {
//        public UserInfo_Mapping()
//        {
//            //这里的X指的是一个实例化的<span style="line-height: 17.1072px; font-family: 'Open Sans', Arial, Helvetica, sans-serif;">UserInfo_Model</span>表格类  
//            Id(x => x.userID).Column("Id");//设置主键，并与数据库的字段映射  
//            Map(x => x.用户名).Column("用户名");//设置X中的某变量与数据库中某字段的映射关系  
//            //Column("密码")里面的字符串一定要和数据库里面的字段名相同，他可以不和userinfo里面的字段相同  
//            Map(x => x.密码).Column("密码");
//            Map(x => x.昵称).Column("昵称");
//            Map(x => x.胜利场数).Column("胜利场数");
//            Map(x => x.失败场数).Column("失败场数");
//            Map(x => x.消费点数).Column("消费点数");
//            Map(x => x.省名).Column("省名");
//            Map(x => x.市).Column("市");
//            Map(x => x.县区).Column("县区");

//            Table("userinfo");//告诉程序我要建立映射关系的数据库表的名字  
//        }
//    }
//}
