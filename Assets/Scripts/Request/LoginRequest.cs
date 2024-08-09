using Common;

public class LoginRequest : BaseRequest
{
    LogInPanel logInPanel;
    void Start()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        logInPanel = GetComponent<LogInPanel>();
    }

    public void SendRequest(string username, string password)
    {
        string data = username + ":" + password;
        SendRequest(data); 
    }

    public override void OnResponse(string data)
    {
        base.OnResponse(data);
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        logInPanel.OnLoginResponse(returnCode);
    }
}
