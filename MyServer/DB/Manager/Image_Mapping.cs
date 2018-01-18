//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MyServer.DB.DBManager
//{
//    class Image_Mapping : ClassMap<Image_Model>
//    {
//        public Image_Mapping()
//        {
//            Id(x => x.ID).Column("Id");//设置主键，并与数据库的字段映射  
//            Map(x => x.用户ID).Column("用户ID");//设置X中的某变量与数据库中某字段的映射关系  

//            Map(x => x.头像).Column("头像");


//            Table("image");//告诉程序我要建立映射关系的数据库表的名字  

//        }
//    }
//}
