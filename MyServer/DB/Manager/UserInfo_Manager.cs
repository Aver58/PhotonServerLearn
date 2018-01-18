//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ExitGames.Logging;
//using GameServer.Model;

//namespace MyServer.DB.DBManager
//{
//    public class UserInfo_Manager
//    {
//        private static readonly ILogger log = LogManager.GetCurrentClassLogger();//定义log输出的接口  

//        /// <summary>  
//        /// 查询SQL服务器中NHibernateHelper规定的数据库（ilovepaohuzi）  
//        /// 中UserInfo_Model对应的映射关系的表中的数据，并返回一个list,  
//        /// 如果list返回成功代表查询成功，如果返回null就表示查询失败  
//        /// </summary>  
//        /// <param name="name"></param>  
//        /// <returns></returns>  
//        public List<UserInfo_Model> 获取全部用户列表()
//        {
//            try
//            {
//                using (var sessin = NHibernateHelper.OpenSessin())
//                //先开始建立 查询线程这个NHibernateHelper中就已经规定了数据库为ilovepaohuzi  
//                {
//                    using (var trsan = sessin.BeginTransaction())//使用线程开始办理事务（开始工作）  
//                    {
//                        //这个方法会自动寻找UserInfo_Model的映射关系类，还记得 class UserInfo_Mapping : ClassMap<UserInfo_Model>这一段吗？  
//                        var dateList = sessin.QueryOver<UserInfo_Model>();//将查询结果放入UserInfo_Model为类型一个接口中，并赋予变量  
//                        trsan.Commit();//结束这个事务  
//                        return dateList.List().ToList();//返回接口中的数据生成的LIST  
//                    }
//                }

//            }
//            catch (Exception e)
//            {
//                //将错误代码输出到log中去  
//                log.Debug("UserInfo_Manager中 (获取全部用户列表) 方法出错：" + e);
//                return null;
//            }

//        }


//        /// <summary>  
//        /// 使用用户名查询用户数据，返回的结果为null代表查询出错，结果为LIST为查询成功  
//        /// list的数量为0表示没有这个用户，数量》=1表示有这个用户  
//        /// </summary>  
//        /// <param username="username"></param>  
//        /// <returns></returns>  
//        public List<UserInfo_Model> 查询某用户_根据用户名(string username)
//        {
//            try
//            {
//                using (var sessin = NHibernateHelper.OpenSessin())//先开始建立 查询线程  
//                {
//                    using (var trsan = sessin.BeginTransaction())//使用线程开始办理事务（开始工作）  
//                    {
//                        var dateList = sessin.QueryOver<UserInfo_Model>().Where(用户数据 => 用户数据.用户名 == username);
//                        //以某条件查询并将查询结果放入TestUser为类型一个接口中，并赋予变量  
//                        trsan.Commit();//结束这个事务  
//                        return dateList.List().ToList();//返回接口中的数据生成的LIST  
//                    }
//                }

//            }
//            catch (Exception e)
//            {
//                //将错误代码输出到log中去  
//                log.Debug("UserInfo_Manager中 (查询某用户_根据用户名) 方法出错：" + e);
//                return null;
//            }

//        }


//        /// <summary>  
//        /// 建立新的用户，如果相同则返回-1，建立出错返回-2，建立成功则返回这个用户分配的ID号  
//        /// </summary>  
//        /// <param name="newUser"></param>  
//        /// <returns></returns>  
//        public int 用户注册(UserInfo_Model 用户注册信息)
//        {
//            try
//            {

//                //要保证   用户注册信息 的数据符合服务器的格式，例如，我们的ID项目是7位数的int,如果他是8位数的数字就会出错  
//                using (var sessin = NHibernateHelper.OpenSessin())//先开始建立 查询线程  
//                {
//                    using (var trsan = sessin.BeginTransaction())//使用线程开始办理事务（开始工作）  
//                    {

//                        //先查询表里有没有相同的用户名，如果有的话则返回-1;  
//                        IList<UserInfo_Model> 查询结果 = 查询某用户_根据用户名(用户注册信息.用户名);
//                        if (查询结果 == null) return (-2);
//                        if (查询结果.Count > 0) return (-1);

//                        //如果没有同名的则存储新用户，并返回新用户的ID号  
//                        sessin.Save(用户注册信息);
//                        trsan.Commit();//结束这个事务  
//                        return 用户注册信息.userID;
//                    }
//                }

//            }
//            catch (Exception e)
//            {
//                //将错误代码输出到log中去  
//                log.Debug("UserInfo_Manager中 (用户注册) 方法出错：" + e);
//                return -2;
//            }

//        }
//        /// <summary>  
//        /// 修改用户资料，需要提供的用户资料的ID和用户名匹配，返回-1为ID和用户名不匹配，或者用户不存在。返回-2为服务器错误，返回0为修改成功  
//        /// </summary>  
//        /// <param name="修改后的用户资料"></param>  
//        /// <returns></returns>  
//        public int 修改用户资料(UserInfo_Model 修改后的用户资料)
//        {
//            try
//            {
//                //先检查用户ID和用户名是否正确  
//                using (var sessin = NHibernateHelper.OpenSessin())//先开始建立 查询线程  
//                {
//                    using (var trsan = sessin.BeginTransaction())//使用线程开始办理事务（开始工作）  
//                    {

//                        //先查询表里有没有相同的用户名，如果有的话则返回-1;  
//                        IList<UserInfo_Model> 查询结果 = 查询某用户_根据用户名(修改后的用户资料.用户名);
//                        //如果没有这个用户或者用户名和ID不同，则返回-1；代表没有这个用户  
//                        //如果查询错误则返回-2  
//                        if (查询结果 == null) return (-2);

//                        if (查询结果.Count <= 0) return (-1);
//                        if (查询结果.Count > 0 && 修改后的用户资料.userID != 查询结果[0].userID) return (-1);
//                        //如果查询结果正常，则开始更新用户数据  
//                        sessin.Update(修改后的用户资料);

//                        trsan.Commit();//结束这个事务  
//                        return 0;
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                //将错误代码输出到log中去  
//                log.Debug("UserInfo_Manager中 (修改用户资料) 方法出错：" + e);
//                return -2;
//            }
//            return -2;
//        }
//    }
//}
