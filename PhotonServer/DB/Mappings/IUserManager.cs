using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 管理接口
/// </summary>
interface IUserManager
{
    void Add(UserInfo user);
    void Update(UserInfo user);
    void Remove(UserInfo user);
    UserInfo GetById(int id);
    UserInfo GetByUsername(string username);
    ICollection<UserInfo> GetAllUsers();
    bool VerifyUser(string username, string password);
}
