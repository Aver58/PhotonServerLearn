using MyServer;
using MyServer.Common;
using MyServer.DB.DBMapping;
using Photon.SocketServer;

public class LoginHandler : BaseHandler
{
    public LoginHandler()
    {
        OpCode = OperateCode.Login;
    }

    /// <summary>
    /// 处理客户端端的登录请求
    /// </summary>
    /// <param name="operationRequest"></param>
    /// <param name="sendParameters"></param>
    /// <param name="peer"></param>
    public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, MyPeer peer)
    {
        string username = DictTool.GetValue(operationRequest.Parameters, (byte)UserCode.Username) as string;
        string password = DictTool.GetValue(operationRequest.Parameters, (byte)UserCode.Password) as string;
        UserManager manager = new UserManager();
        bool isSuccess = manager.VerifyUser(username, password);

        OperationResponse response = new OperationResponse(operationRequest.OperationCode);
        if (isSuccess)
        {
            response.ReturnCode = (short)ReturnCode.Success;
            peer.username = username;
        }
        else
        {
            response.ReturnCode = (short)ReturnCode.Failed;
        }
        peer.SendOperationResponse(response, sendParameters);
    }
}