using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using MyServer.Common;
using MyServer.DB.DBMapping;
using NHibernate;
using NHibernate.Cfg;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    public class MyGameServer : ApplicationBase
    {

        //Handler集合:下发给子Handler去处理响应
        public Dictionary<OperationCode, BaseHandler> HandlerDict = new Dictionary<OperationCode, BaseHandler>();
        public new static MyGameServer Instance{ get ; private set ; }

        /// <summary>
        /// 当有客户端连接上服务器时，将引用这个方法，建立一个客户端处理类，我们叫它ClientPeer,他是一个继承自PeerBase的自定义类 
        /// </summary>
        /// <param name="initRequest"></param>
        /// <returns></returns>
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new MyPeer(initRequest);
        }
        /// <summary>  
        /// 服务器初始化  
        /// </summary>  
        /// 当有客户端连接上服务器时，将引用这个方法，建立一个客户端处理类，我们叫它ClientPeer,他是一个继承自PeerBase的自定义类  
        protected override void Setup()
        {
            //初始化各个Handler
            Instance = this;
            //InitHandler();
            InitConfiguration();
            InitLogging();
            LogInfo("-----------------------");
            LogInfo("Server is Setup");
        }

        /// <summary>  
        /// 服务器关闭  
        /// </summary> 
        /// 当有客户端连接上服务器时，将引用这个方法，建立一个客户端处理类，我们叫它ClientPeer,他是一个继承自PeerBase的自定义类  
        protected override void TearDown()
        {
            LogInfo("Server is Down");
        }
        /// <summary>
        /// 初始化子Handler
        /// </summary>
        public void InitHandler()
        {
            DefaultHandler defaultHandler = new DefaultHandler();
            HandlerDict.Add(defaultHandler.OpCode, defaultHandler);

            LoginHandler loginHandler = new LoginHandler();
            HandlerDict.Add(loginHandler.OpCode, loginHandler);

            RegisterHandler registerHandler = new RegisterHandler();
            HandlerDict.Add(registerHandler.OpCode, registerHandler);
        }

        
       
        /// <summary>
        /// 初始化配置
        /// </summary>
        void InitConfiguration()
        {
            var configuration = new Configuration();
            configuration.Configure();                                  //解析DefaultHibernateCfgFileName = "hibernate.cfg.xml";
            configuration.AddAssembly(Assembly.GetExecutingAssembly()); //解析 映射文件  User.hbm.xml 、、把所有的.hbm.xml文件解析一下Adds all of the assembly's embedded resources whose names end with .hbm.xml.
           
        }
        #region 数据库操作
        ////创建连接session进行添加操作
        //ISessionFactory sessionFactory = null;
        //ISession session = null;
        //try
        //{
        //    sessionFactory = configuration.BuildSessionFactory();

        //    session = sessionFactory.OpenSession();//打开一个跟数据库的会话，即连接数据库

        //    //进行一些操作
        //    UserInfo user = new UserInfo() { Username = "yeet", Password = "123564" };

        //    session.Save(user);
        //    session.Flush();
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e);
        //}
        //finally
        //{
        //    if (session != null)
        //    {
        //        session.Close();
        //    }
        //    if (sessionFactory != null)
        //    {
        //        sessionFactory.Close();
        //    }
        //}
        ////用NHibernate进行事务操作
        ////什么时候使用事务?
        ////在任何时候都要使用事务，即使是在读取、查询数据的时候，为什么呢？因为你不清楚数据库什么时候操作失败，如何恢复原来数据。
        ////而NHibernate中的事务（可以通过 transaction.Rollback()方法），帮助我们完成这些事情。
        //ITransaction transaction = session.BeginTransaction();
        ////进行操作
        //UserInfo user1 = new UserInfo() { Username = "cydr34", Password = "g3463" };

        //session.Save(user1);
        //transaction.Commit();

        ////使用完事务需要关闭
        //transaction.Dispose();

        //UserInfo user2 = new UserInfo() { Username = "qwqjer", Password = "2544" };
        //IUserManager userManager = new UserManager();
        //userManager.Add(user2);

        #endregion

        #region 日志功能  

        //接口变量的定义
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();//添加一个静态只读字段log并初始化

        /// <summary>  
        /// 初始化日志  
        /// </summary>  
        private void InitLogging()
        {
            // 日志的初始化
            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(
                                                                    Path.Combine(this.ApplicationRootPath, "bin_Win64"), "log");
            //LogInfo("Photon:ApplicationLogPath" + GlobalContext.Properties["Photon:ApplicationLogPath"].ToString());
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            //LogInfo("configFileInfo.DirectoryName" + configFileInfo.DirectoryName);
            if (configFileInfo.Exists)
            {
                ExitGames.Logging.LogManager.SetLoggerFactory(ExitGames.Logging.Log4Net.Log4NetLoggerFactory.Instance);//让photon知道使用的是Log4NetLog插件
                XmlConfigurator.ConfigureAndWatch(configFileInfo);//让log4net这个插件读取配置文件
            }
            log.Info("初始化完成！");
            //GlobalContext.Properties["LogFileName"] = this.ApplicationName; //this.ApplicationName+"2.0";  
            //XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(this.BinaryPath, "log4net.config")));
        }
        /// <summary>  
        /// 日志输出  
        /// </summary>  
        /// <param name="str"></param>  
        public static void LogInfo(string str)
        {
            log.Info(str);
        }
       
        #endregion
    }
}
