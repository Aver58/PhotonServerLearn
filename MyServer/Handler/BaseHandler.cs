using MyServer;
using MyServer.Common;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 服务器端新建处理各个请求的 BaseHandler基类：
/// </summary>
public abstract class BaseHandler
{
    public OperateCode OpCode;//请求代码

    /// <summary>
    /// 响应请求的抽象类
    /// </summary>
    /// <param name="operationRequest"></param>
    /// <param name="sendParameters"></param>
    /// <param name="peer"></param>
    public abstract void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyPeer peer);
}

//新建一个默认的Handler：
class DefaultHandler : BaseHandler
{
    public DefaultHandler()
    {
        OpCode = OperateCode.Default;
    }
    public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyPeer peer)
    {
        switch (operationRequest.OperationCode)//通过OpCode区分请求
        {
            case 1:
                Dictionary<byte, object> data = operationRequest.Parameters;
                object intValue;
                object stringValue;
                data.TryGetValue(1, out intValue);
                data.TryGetValue(2, out stringValue);
                MyGameServer.LogInfo("服务器：得到客户端传来的参数数据是:" + intValue.ToString() + "  " + stringValue.ToString());

                //向客户端作出响应
                OperationResponse opResponse = new OperationResponse(1);

                //向客户端发送参数
                Dictionary<byte, object> data2 = new Dictionary<byte, object>();
                data2.Add(1, 300);
                data2.Add(2, "服务器：给客户端发送参数数据");
                opResponse.SetParameters(data2);
                peer.SendOperationResponse(opResponse, sendParameters);//给客户端一个响应，只能在这里调用，在其它地方无效

                ///客户端在没有向服务器端发送请求，但服务器端想通知或发送数据给客户端时，就可以使用SendEvent方法，
                ///SendEvent方法可以在服务器任何地方调用。
                //EventData ed = new EventData(1);
                //ed.Parameters = data2;
                //SendEvent(ed, new SendParameters());
                break;
            case 2:
                break;
            default:
                break;
        }
    }
}
