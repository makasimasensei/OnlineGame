using Common;

public class LoginRequest : BaseRequest
{
    LogInPanel logInPanel;
    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        logInPanel = GetComponent<LogInPanel>();
        base.Awake();
    }

    /// <summary>
    /// Send request.
    /// </summary>
    /// <param name="username">Username.</param>
    /// <param name="password">Password.</param>
    public void SendRequest(string username, string password)
    {
        string data = username + ":" + password;
        SendRequest(data);
    }

    /// <summary>
    /// Handle response data.
    /// </summary>
    /// <param name="data">Data.</param>
    public override void OnResponse(string data)
    {
        base.OnResponse(data);
        string[] strs = data.Split(',');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        logInPanel.OnLoginResponse(returnCode);
        if (returnCode == ReturnCode.Success)
        {
            string username = strs[1];
            int totolCount = int.Parse(strs[2]);
            int winCount = int.Parse(strs[3]);
            UserData ud = new UserData(username, totolCount, winCount);
            gameFacade.SetUserData(ud);
        }
    }
}
