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
    public OperationCode OpCode;

    public abstract void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyPeer peer);
}

//新建一个默认的Handler：
class DefaultHandler : BaseHandler
{
    public DefaultHandler()
    {
        OpCode = OperationCode.Default;
    }
    public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyPeer peer)
    {
        
    }
}
