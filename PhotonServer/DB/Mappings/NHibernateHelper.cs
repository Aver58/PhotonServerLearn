using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;

/// <summary>
/// NHibernateHelper管理会话工厂
/// </summary>
public class NHibernateHelper
{
    private static ISessionFactory _sessionFactory;
    private static readonly object lockObj = new object();

    private NHibernateHelper() { }
    /// <summary>
    /// 单例
    /// </summary>
    private static ISessionFactory SessionFactory
    {
        get
        {
            if (_sessionFactory == null)
            {
                lock (lockObj)
                {
                    if (_sessionFactory == null)
                    {
                        var configuration = new Configuration();
                        configuration.Configure();
                        configuration.AddAssembly(Assembly.GetExecutingAssembly());
                        _sessionFactory = configuration.BuildSessionFactory();
                    }
                }
            }
            return _sessionFactory;
        }
    }
    /// <summary>
    /// 打开一个跟数据库的会话，即连接数据库
    /// </summary>
    /// <returns></returns>
    public static ISession OpenSession()
    {
        return SessionFactory.OpenSession();
    }
    /// <summary>
    /// 在任何时候都要使用事务，即使是在读取、查询数据的时候，为什么呢？因为你不清楚数据库什么时候操作失败，如何恢复原来数据。
    /// 而NHibernate中的事务（可以通过 transaction.Rollback()方法），帮助我们完成这些事情。
    /// </summary>
    //public static void Events()
    //{
    //    ITransaction transaction = session.BeginTransaction();
    //    //进行操作
    //    User user1 = new User() { Username = "cydr34", Password = "g3463" };
    //    session.Save(user1);
    //    transaction.Commit();

    //    //使用完事务需要关闭
    //    transaction.Dispose();
    //}
}
