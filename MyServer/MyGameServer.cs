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

        //Handler集合
        public Dictionary<OperationCode, BaseHandler> HandlerDict = new Dictionary<OperationCode, BaseHandler>();
        public static MyGameServer Instance{get;private set;}
        /// <summary>  
        /// 服务器初始化  
        /// </summary>  
        /// 当有客户端连接上服务器时，将引用这个方法，建立一个客户端处理类，我们叫它ClientPeer,他是一个继承自PeerBase的自定义类  

        protected override void Setup()
        {
            //初始化各个Handler
            Instance = this;
            InitHandler();
            InitConfiguration();
            InitLogging();
            LogInfo("-----------------------");
            LogInfo("Server is Setup");
        }

        public void InitHandler()
        {
            DefaultHandler defaultHandler = new DefaultHandler();
            HandlerDict.Add(defaultHandler.OpCode, defaultHandler);

            LoginHandler loginHandler = new LoginHandler();
            HandlerDict.Add(loginHandler.OpCode, loginHandler);

            RegisterHandler registerHandler = new RegisterHandler();
            HandlerDict.Add(registerHandler.OpCode, registerHandler);
        }

        //当有客户端连接上服务器时，将引用这个方法，建立一个客户端处理类，我们叫它ClientPeer,他是一个继承自PeerBase的自定义类  
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new MyPeer(initRequest);
        }
       
        /// <summary>
        /// 初始化配置
        /// </summary>
        void InitConfiguration()
        {
            var configuration = new Configuration();
            configuration.Configure();                                  //解析Nhibernate.cfg.xml 
            configuration.AddAssembly(Assembly.GetExecutingAssembly()); //解析 映射文件  User.hbm.xml 

            //创建连接session进行添加操作
            ISessionFactory sessionFactory = null;
            ISession session = null;
            try
            {
                sessionFactory = configuration.BuildSessionFactory();

                session = sessionFactory.OpenSession();//打开一个跟数据库的会话，即连接数据库

                //进行一些操作
                UserInfo user = new UserInfo() { userName = "yeet", password = "123564" };

                session.Save(user);
                session.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (session != null)
                {
                    session.Close();
                }
                if (sessionFactory != null)
                {
                    sessionFactory.Close();
                }
            }

            ITransaction transaction = session.BeginTransaction();
            //进行操作
            UserInfo user1 = new UserInfo() { userName = "cydr34", password = "g3463" };

            session.Save(user1);
            transaction.Commit();

            //使用完事务需要关闭
            transaction.Dispose();

            UserInfo user2 = new UserInfo() { userName = "qwqjer", password = "2544" };
            IUserManager userManager = new UserManager();
            userManager.Add(user2);
        }
        /// <summary>  
        /// 服务器关闭  
        /// </summary> 
        /// 当有客户端连接上服务器时，将引用这个方法，建立一个客户端处理类，我们叫它ClientPeer,他是一个继承自PeerBase的自定义类  
        protected override void TearDown()
        {
            LogInfo("Server is Down");
        }
        #region 日志功能  

        //接口变量的定义
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();//添加一个静态只读字段log并初始化

        /// <summary>  
        /// 初始化日志  
        /// </summary>  
        private void InitLogging()
        {
            // 日志的初始化
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(
                                                                    Path.Combine(this.ApplicationRootPath, "bin_Win64"), "log");
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
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
