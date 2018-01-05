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

        }
        /// <summary>
        /// 处理客户端的请求
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            BaseHandler handler = DictTool.GetValue<OperationCode, BaseHandler>(MyGameServer.Instance.HandlerDict, (OperationCode)operationRequest.OperationCode);
            if (handler != null)
            {
                handler.OnOperationRequest(operationRequest, sendParameters, this);
            }
            else
            {
                BaseHandler defaultHandler = DictTool.GetValue<OperationCode, BaseHandler>(MyGameServer.Instance.HandlerDict, OperationCode.Default);
                defaultHandler.OnOperationRequest(operationRequest, sendParameters, this);
            }
        }
    }
}
