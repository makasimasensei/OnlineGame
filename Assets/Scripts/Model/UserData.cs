public class UserData
{
    public UserData(string username, int totalCount, int winCount)
    {
        UserName = username;
        TotalCount = totalCount;
        WinCount = winCount;
    }

    public string UserName { get;private set; }
    public int TotalCount { get; private set; }
    public int WinCount { get; private set; }

}
