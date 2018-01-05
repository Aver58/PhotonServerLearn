using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer.RequestEnum
{
    public enum RequestEnum : byte
    {
        loginRes, //注册请求_通讯类型,
        registerRes,//登录请求_通讯类型
    }
}
