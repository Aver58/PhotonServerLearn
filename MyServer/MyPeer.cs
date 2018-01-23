using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotonHostRuntimeInterfaces;
using MyServer.Common;

namespace MyServer
{
    /// <summary>
    /// 与客户端通信的类MyClientPeer||ClientPeer继承PeerBase
    /// </summary>
    public class MyPeer : ClientPeer
    {
        public string username;//用户名

        /// <summary>  
        /// 构造  
        /// </summary>  
        /// <param name="initRequest"></param>  
        public MyPeer(InitRequest initRequest) : base(initRequest)
        {

        }


        /// <summary>  
        /// 客户端断开连接  
        /// </summary>  
        /// <param name="reasonCode"></param>  
        /// <param name="reasonDetail"></param>  
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            MyGameServer.LogInfo("客户端断开连接");
        }


        /// <summary>
        /// 处理客户端的请求
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {

            BaseHandler handler = DictTool.GetValue(MyGameServer.Instance.HandlerDict, (OperateCode)operationRequest.OperationCode);
            if (handler != null)//如果有这个handler的话就用子handler处理
            {
                handler.OnOperationRequest(operationRequest, sendParameters, this);
            }
            else//如果没有handler的话就用默认handler处理
            {
                BaseHandler defaultHandler = DictTool.GetValue(MyGameServer.Instance.HandlerDict, OperateCode.Default);
                defaultHandler.OnOperationRequest(operationRequest, sendParameters, this);
            }

        }
    }
}
