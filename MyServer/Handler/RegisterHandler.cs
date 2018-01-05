using MyServer;
using MyServer.Common;
using MyServer.DB.DBMapping;
using Photon.SocketServer;

class RegisterHandler : BaseHandler
{
    public RegisterHandler()
    {
        OpCode = OperationCode.Register;
    }
    /// <summary>
    /// 处理客户端的注册请求
    /// </summary>
    /// <param name="operationRequest"></param>
    /// <param name="sendParameters"></param>
    /// <param name="peer"></param>
    public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyPeer peer)
    {
        string username = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Username) as string;
        string password = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Password) as string;
        UserManager manager = new UserManager();
        UserInfo user = manager.GetByUsername(username);
        OperationResponse response = new OperationResponse(operationRequest.OperationCode);
        if (user == null)
        {
            user = new UserInfo() { userName = username, password = password };
            manager.Add(user);
            response.ReturnCode = (short)ReturnCode.Success;
        }
        else
        {
            response.ReturnCode = (short)ReturnCode.Failed;
        }
        peer.SendOperationResponse(response, sendParameters);
    }
}