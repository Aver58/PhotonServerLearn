﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Common
{
    public enum OperateCode : byte //区分请求和响应的类型 
    {
        Default,//默认请求
        Login, //登录 
        Register //注册
    }

    public enum ReturnCode : short //服务器返回的类型
    {
        Success,//成功
        Failed //失败
    }

    public enum UserCode : byte //区分传送数据的时候，参数的类型
    {
        Username,//用户名
        Password,  //密码
    }

    /// <summary>
    /// 获取字典的值
    /// </summary>
    public class DictTool
    {
        public static T2 GetValue<T1, T2>(Dictionary<T1, T2> dict, T1 key)
        {
            T2 value;
            bool isSuccess = dict.TryGetValue(key, out value);
            if (isSuccess)
            {
                return value;
            }
            else
            {
                return default(T2);
            }
        }
    }
}
