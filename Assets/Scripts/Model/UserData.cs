public class UserData
{
    public UserData(string userData)
    {
        string[] strs = userData.Split(',');
        Id = int.Parse(strs[0]);
        UserName = strs[1];
        TotalCount = int.Parse(strs[2]);
        WinCount = int.Parse(strs[3]);
    }

    public UserData(string username, int totalCount, int winCount)
    {
        UserName = username;
        this.TotalCount = totalCount;
        this.WinCount = winCount;
    }

    public UserData(int id, string username, int totalCount, int winCount)
    {
        Id = id;
        UserName = username;
        TotalCount = totalCount;
        WinCount = winCount;
    }

    public int Id {  get; private set; }
    public string UserName { get;private set; }
    public int TotalCount { get; private set; }
    public int WinCount { get; private set; }

}
