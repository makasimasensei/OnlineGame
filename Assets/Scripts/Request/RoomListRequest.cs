using Common;
using System.Collections.Generic;

public class RoomListRequest : BaseRequest
{
    RoomListPanel roomListPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.RoomList;
        roomListPanel = GetComponent<RoomListPanel>();
        base.Awake();
    }

    public override void SendRequest()
    {
        base.SendRequest("r");
    }

    public override void OnResponse(string data)
    {
        base.OnResponse(data);
        if (data == "0") return;
        List<UserData> udList = new List<UserData>();
        string[] udArray = data.Split('|');
        foreach (string ud in udArray)
        {
            string[] strs = ud.Split(',');
            udList.Add(new UserData(strs[0], int.Parse(strs[1]), int.Parse(strs[2])));
        }
        roomListPanel.LoadRoomItemSync(udList);
    }
}
