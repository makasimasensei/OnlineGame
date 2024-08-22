using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text username;
    public Text totoalCount;
    public Text winCount;
    public Button joinButton;

    int id;
    RoomListPanel roomListPanel;

    private void Start()
    {
        if (joinButton != null)
        {
            joinButton.onClick.AddListener(OnJoinClick);
        }
    }

    /// <summary>
    /// Set the room information.
    /// </summary>
    /// <param name="username">Username</param>
    /// <param name="totalcount">The total number of games.</param>
    /// <param name="wincount">Total wins.</param>
    public void SetRoomInfo(int id, string username, int totalcount, int wincount, RoomListPanel roomListPanel)
    {
        this.id = id;
        this.username.text = username;
        this.totoalCount.text = "总场数：" + totalcount.ToString();
        this.winCount.text = "胜利：" + wincount.ToString();
        this.roomListPanel = roomListPanel;
    }

    void OnJoinClick()
    {
        roomListPanel.OnJoinClick(id);
    }

    public void DestroySelf()
    {
        GameObject.Destroy(this.gameObject);
    }
}
