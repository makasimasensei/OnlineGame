using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoomListPanel : BasePanel
{
    RectTransform battleRes;
    RectTransform roomList;
    VerticalLayoutGroup verticalLayoutGroup;
    GameObject roomItemPrefab;

    void Start()
    {
        battleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        verticalLayoutGroup = transform.Find("RoomList/ScrollRect/Layout").GetComponent<VerticalLayoutGroup>();
        roomItemPrefab = Resources.Load("UIPanel/RoomItem") as GameObject;
        transform.Find("RoomList").Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
        transform.Find("RoomList").Find("CreateButton").GetComponent<Button>().onClick.AddListener(OnCreateRoomClick);

        EnterAnim();
    }

    /// <summary>
    /// Override OnEnter.
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        SetBattleRes();
        if (battleRes != null) EnterAnim();
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

    /// <summary>
    /// Load the items of the room.
    /// </summary>
    /// <param name="count"></param>
    void LoadRoomItem(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab);
            roomItem.transform.SetParent(verticalLayoutGroup.transform);
            roomItem.transform.localScale = Vector3.one;
        }
    }

    /// <summary>
    /// Click to create room.
    /// </summary>
    void OnCreateRoomClick()
    {
        uiManager.PushPanel(UIPanelType.Room);
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        LoadRoomItem(1);
    //    }
    //}
}
