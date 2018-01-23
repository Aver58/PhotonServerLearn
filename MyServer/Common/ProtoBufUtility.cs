using MyServer;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// ProtoBuf使用
/// </summary>
public class ProtoBufUtility
{
    #region Public Function
    /// <summary>
    /// 数据加字节头操作
    /// </summary>
    /// <returns>数据结果.</returns>
    /// <param name="data">源数据.</param>
    byte[] LengthEncode(byte[] data)
    {
        //内存流实例
        using (MemoryStream ms = new MemoryStream())
        {
            //二进制流写操作实例
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                //先写入字节长度
                bw.Write(data.Length);
                //再写入所有数据
                bw.Write(data);
                //临时结果
                byte[] result = new byte[ms.Length];
                //将写好的流数据放入临时结果
                Buffer.BlockCopy(ms.GetBuffer(), 0, result, 0, (int)ms.Length);
                //返回临时结果
                return result;
            }
        }
    }
    /// <summary>
    /// 数据解析，拆解字节头，获取数据.
    /// </summary>
    /// <returns>源数据.</returns>
    /// <param name="cache">缓存数据.</param>
    byte[] LengthDecode(ref List<byte> cache)
    {
        //如果字节数小于4，出现异常
        if (cache.Count < 4)
            return null;
        //内存流实例
        using (MemoryStream ms = new MemoryStream(cache.ToArray()))
        {
            //二进制流读操作实例
            using (BinaryReader br = new BinaryReader(ms))
            {
                //先读取数据长度，一个int值
                int realMsgLength = br.ReadInt32();
                //如果未接收全数据，下次继续接收
                if (realMsgLength > ms.Length - ms.Position)
                {
                    return null;
                }
                //接收完，读取所有数据
                byte[] result = br.ReadBytes(realMsgLength);
                //清空缓存
                cache.Clear();
                //返回结果
                return result;
            }
        }
    }
    /// <summary>
    /// 序列化数据.
    /// </summary>
    /// <param name="mod">数据对象.</param>
    private byte[] Serialize(NetModel mod)
    {
        try
        {
            //内存流实例
            using (MemoryStream ms = new MemoryStream())
            {
                //ProtoBuf协议序列化数据对象
                ProtoBuf.Serializer.Serialize<NetModel>(ms, mod);
                //创建临时结果数组
                byte[] result = new byte[ms.Length];
                //调整游标位置为0
                ms.Position = 0;
                //开始读取，从0到尾
                ms.Read(result, 0, result.Length);
                //返回结果
                return result;
            }
        }
        catch (Exception ex)
        {
            MyGameServer.LogInfo("Error: " + ex.ToString());
            return null;
        }
    }

    /// <summary>
    /// 反序列化数据.
    /// </summary>
    /// <returns>数据对象.</returns>
    /// <param name="data">源数据.</param>
    private NetModel DeSerialize(byte[] data)
    {
        try
        {
            //内存流实例
            using (MemoryStream ms = new MemoryStream(data))
            {
                //调整游标位置
                ms.Position = 0;
                //ProtoBuf协议反序列化数据
                NetModel mod = ProtoBuf.Serializer.Deserialize<NetModel>(ms);
                //返回数据对象
                return mod;

            }
        }
        catch (Exception ex)
        {
            MyGameServer.LogInfo("Error: " + ex.ToString());
            return null;
        }
    }
    #endregion

    ///// <summary>
    ///// 序列化
    ///// </summary>
    ///// <param name="model"></param>
    ///// <param name="type"></param>
    ///// <returns></returns>
    //public static byte[] Serialize(object model, Type type)
    //{
    //    using (MemoryStream ms = new MemoryStream())
    //    {
    //        Serializer.Serialize(ms, type, model);
    //        byte[] data = new byte[ms.Length];
    //        ms.Position = 0;
    //        ms.Read(data, 0, data.Length);
    //        return data;
    //    }
    //}

    //public static object Deserialize(Type type, Byte[] data)
    //{
    //    using (MemoryStream ms = new MemoryStream(data))
    //    {
    //        return Serializer.Deserialize(type, ms);
    //    }
    //}
}

