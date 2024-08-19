using Common;

public class CreateRoomRequest : BaseRequest
{
    RoomPanel roomPanel;

    /// <summary>
    /// Override Awake.
    /// </summary>
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.CreateRoom;
        roomPanel = GetComponent<RoomPanel>();
        base.Awake();
    }

    public override void SendRequest(string data)
    {
        base.SendRequest("r");
    }

    public override void OnResponse(string data)
    {
        base.OnResponse(data);
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        if (returnCode == ReturnCode.Success)
        {
            roomPanel.SetLocalPlayerResSync();
        }
    }
}
