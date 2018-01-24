using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.DB.DBMapping
{
    /// <summary>
    /// 数据库事务操作
    /// </summary>
    class UserManager : IUserManager
    {
        /// <summary>
        /// 向数据库添加用户
        /// </summary>
        /// <param name="user"></param>
        public void Add(UserInfo user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
                session.Close();
            }

        }
        /// <summary>
        /// 更新玩家信息
        /// </summary>
        /// <param name="user"></param>
        public void Update(UserInfo user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(user);
                    transaction.Commit();
                }
            }
        }
        /// <summary>
        /// 移除玩家信息
        /// </summary>
        /// <param name="user"></param>
        public void Remove(UserInfo user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(user);
                    transaction.Commit();
                }
            }
        }
        /// <summary>
        /// 获得玩家的ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserInfo GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    UserInfo user = session.Get<UserInfo>(id);
                    transaction.Commit();
                    return user;
                }
            }
        }


        /// <summary>
        /// 访问数据库、是否有该用户名
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserInfo GetByUsername(string username)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                UserInfo user = session.
                    CreateCriteria(typeof(UserInfo)).
                    Add(Restrictions.Eq("Username", username)).
                    UniqueResult<UserInfo>();
                return user;
            }
        }

        /// <summary>
        /// 获得所有的玩家
        /// </summary>
        /// <returns></returns>
        public ICollection<UserInfo> GetAllUsers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<UserInfo> users = session.CreateCriteria(typeof(UserInfo)).List<UserInfo>();
                return users;
            }
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool VerifyUser(string username, string password)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                UserInfo user = session
                    .CreateCriteria(typeof(UserInfo))
                    .Add(Restrictions.Eq("Username", username))
                    .Add(Restrictions.Eq("Password", password))
                    .UniqueResult<UserInfo>();
                if (user == null) return false;
                return true;
            }
        }
    }
}
