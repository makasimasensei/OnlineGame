using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListPanel : BasePanel
{
    RectTransform battleRes;
    RectTransform roomList;
    VerticalLayoutGroup verticalLayoutGroup;
    GameObject roomItemPrefab;
    RoomListRequest roomListRequest;
    List<UserData> udList = null;

    void Start()
    {
        battleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        verticalLayoutGroup = transform.Find("RoomList/ScrollRect/Layout").GetComponent<VerticalLayoutGroup>();
        roomItemPrefab = Resources.Load("UIPanel/RoomItem") as GameObject;
        transform.Find("RoomList").Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
        transform.Find("RoomList").Find("CreateButton").GetComponent<Button>().onClick.AddListener(OnCreateRoomClick);
        roomListRequest = GetComponent<RoomListRequest>();
        EnterAnim();
    }

    private void Update()
    {
        if (this.udList != null)
        {
            LoadRoomItem(udList);
            udList = null;
        }
    }

    /// <summary>
    /// Override OnEnter.
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        SetBattleRes();
        if (battleRes != null) EnterAnim();
        if(roomListRequest == null) roomListRequest = GetComponent<RoomListRequest>();
        roomListRequest.SendRequest();
    }

    /// <summary>
    /// Override OnPause.
    /// </summary>
    public override void OnPause()
    {
        base.OnPause();
        HideAnim();
    }

    /// <summary>
    /// Override OnResume.
    /// </summary>
    public override void OnResume()
    {
        base.OnResume();
        EnterAnim();
        roomListRequest.SendRequest();
    }

    /// <summary>
    /// Override OnExit.
    /// </summary>
    public override void OnExit()
    {
        base.OnExit();
        HideAnim();
    }

    /// <summary>
    /// Click the close button,
    /// </summary>
    void OnCloseClick()
    {
        PlayClickSound();
        uiManager.PopPanel();
    }

    /// <summary>
    /// Enter the animation.
    /// </summary>
    void EnterAnim()
    {
        gameObject.SetActive(true);
        battleRes.localPosition = new Vector3(-1000, 0, 0);
        battleRes.DOLocalMoveX(-690, 0.4f);

        roomList.localPosition = new Vector3(1000, 0, 0);
        roomList.DOLocalMoveX(200, 0.4f);
    }

    /// <summary>
    /// Hide the animation.
    /// </summary>
    void HideAnim()
    {
        battleRes.DOLocalMoveX(-1000, 0.4f);
        roomList.DOLocalMoveX(1000, 0.4f).OnComplete(() => gameObject.SetActive(false));
    }

    /// <summary>
    /// Set the text of battle interface.
    /// </summary>
    void SetBattleRes()
    {
        UserData userData = gameFacade.GetUserData();
        transform.Find("BattleRes/Username").GetComponent<Text>().text = userData.UserName;
        transform.Find("BattleRes/TotalCount").GetComponent<Text>().text = "总场数：" + userData.TotalCount.ToString();
        transform.Find("BattleRes/WinCount").GetComponent<Text>().text = "胜利：" + userData.WinCount.ToString();
    }

    public void LoadRoomItemSync(List<UserData> udList)
    {
        this.udList = udList;
    }

    /// <summary>
    /// Load the items of the room.
    /// </summary>
    /// <param name="count"></param>
    void LoadRoomItem(List<UserData> udList)
    {
        RoomItem[] riArray = verticalLayoutGroup.GetComponentsInChildren<RoomItem>();
        foreach (RoomItem ri in riArray)
        {
            ri.DestroySelf();
        }

        int count = udList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab);
            roomItem.transform.SetParent(verticalLayoutGroup.transform);
            roomItem.transform.localScale = Vector3.one;
            UserData userData = udList[i];
            roomItem.GetComponent<RoomItem>().SetRoomInfo(userData.UserName, userData.TotalCount, userData.WinCount);
        }
    }

    /// <summary>
    /// Click to create room.
    /// </summary>
    void OnCreateRoomClick()
    {
        uiManager.PushPanel(UIPanelType.Room);
    }
}
