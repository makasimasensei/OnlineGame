using Common;
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
    CreateRoomRequest createRoomRequest;
    JoinRoomRequest joinRoomRequest;
    List<UserData> udList = null;

    UserData userData1 = null;
    UserData userData2 = null;

    void Start()
    {
        battleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        verticalLayoutGroup = transform.Find("RoomList/ScrollRect/Layout").GetComponent<VerticalLayoutGroup>();
        roomItemPrefab = Resources.Load("UIPanel/RoomItem") as GameObject;
        transform.Find("RoomList").Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
        transform.Find("RoomList").Find("CreateButton").GetComponent<Button>().onClick.AddListener(OnCreateRoomClick);
        transform.Find("RoomList").Find("RefreshButton").GetComponent<Button>().onClick.AddListener(OnRefreshClick);
        roomListRequest = GetComponent<RoomListRequest>();
        createRoomRequest = GetComponent<CreateRoomRequest>();
        joinRoomRequest = GetComponent<JoinRoomRequest>();
        EnterAnim();
    }

    private void Update()
    {
        if (this.udList != null)
        {
            LoadRoomItem(udList);
            udList = null;
        }
        if (userData1 != null&& userData2!=null)
        {
            userData1 = null;
            userData2 = null;
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
        if (roomListRequest == null) roomListRequest = GetComponent<RoomListRequest>();
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
        transform.Find("BattleRes/TotalCount").GetComponent<Text>().text = "�ܳ�����" + userData.TotalCount.ToString();
        transform.Find("BattleRes/WinCount").GetComponent<Text>().text = "ʤ����" + userData.WinCount.ToString();
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
            roomItem.GetComponent<RoomItem>().SetRoomInfo(userData.Id, userData.UserName, userData.TotalCount, userData.WinCount, this);
        }
    }

    public void OnJoinClick(int id)
    {
        joinRoomRequest.SendRequest(id);
    }

    public void OnJoinResponse(ReturnCode returnCode, UserData ud1, UserData ud2)
    {
        switch (returnCode)
        {
            case ReturnCode.NotFound:
                uiManager.ShowMessageSync("Room not found");
                break;
            case ReturnCode.Fail:
                uiManager.ShowMessageSync("Room can not join.");
                break;
            case ReturnCode.Success:
                this.userData1 = ud1;
                this.userData2 = ud2;
                break;
        }
    }

    /// <summary>
    /// Click to create room.
    /// </summary>
    void OnCreateRoomClick()
    {
        BasePanel panel = uiManager.PushPanel(UIPanelType.Room);
        createRoomRequest.SetPanel(panel);
        createRoomRequest.SendRequest();
    }

    void OnRefreshClick()
    {
        roomListRequest.SendRequest();
    }
}
