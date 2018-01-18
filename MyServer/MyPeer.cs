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
            //BaseHandler handler = DictTool.GetValue(MyGameServer.Instance.HandlerDict, (OperationCode)operationRequest.OperationCode);
            //if (handler != null)//如果有这个handler的话就用子handler处理
            //{
            //    handler.OnOperationRequest(operationRequest, sendParameters, this);
            //}
            //else//如果没有handler的话就用默认handler处理
            //{
            //    BaseHandler defaultHandler = DictTool.GetValue(MyGameServer.Instance.HandlerDict, OperationCode.Default);
            //    defaultHandler.OnOperationRequest(operationRequest, sendParameters, this);
            //}
            switch (operationRequest.OperationCode)//通过OpCode区分请求
            {
                case 1:
                    Dictionary<byte, object> data = operationRequest.Parameters;
                    object intValue;
                    object stringValue;
                    data.TryGetValue(1, out intValue);
                    data.TryGetValue(2, out stringValue);
                    MyGameServer.LogInfo("服务器：得到客户端传来的参数数据是:" + intValue.ToString()+"  " + stringValue.ToString());

                    //向客户端作出响应
                    OperationResponse opResponse = new OperationResponse(1);

                    //向客户端发送参数
                    Dictionary<byte, object> data2 = new Dictionary<byte, object>();
                    data2.Add(1, 300);
                    data2.Add(2, "服务器：给客户端发送参数数据");
                    opResponse.SetParameters(data2);

                    SendOperationResponse(opResponse, sendParameters);//给客户端一个响应，只能在这里调用，在其它地方无效

                    ///客户端在没有向服务器端发送请求，但服务器端想通知或发送数据给客户端时，就可以使用SendEvent方法，SendEvent方法可以在服务器任何地方调用。
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
}
