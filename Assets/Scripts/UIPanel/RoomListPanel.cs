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

    public override void OnEnter()
    {
        base.OnEnter();
        SetBattleRes();
        if (battleRes != null) EnterAnim();
    }

    public override void OnPause()
    {
        base.OnPause();
        HideAnim();
    }

    public override void OnResume()
    {
        base.OnResume();
        EnterAnim();
    }

    public override void OnExit()
    {
        base.OnExit();
        HideAnim();
    }

    void OnCloseClick()
    {
        PlayClickSound();
        uiManager.PopPanel();
    }

    void EnterAnim()
    {
        gameObject.SetActive(true);
        battleRes.localPosition = new Vector3(-1000, 0, 0);
        battleRes.DOLocalMoveX(-690, 0.4f);

        roomList.localPosition = new Vector3(1000, 0, 0);
        roomList.DOLocalMoveX(200, 0.4f);
    }

    void HideAnim()
    {
        battleRes.DOLocalMoveX(-1000, 0.4f);
        roomList.DOLocalMoveX(1000, 0.4f).OnComplete(() => gameObject.SetActive(false));
    }

    void SetBattleRes()
    {
        UserData userData = gameFacade.GetUserData();
        transform.Find("BattleRes/Username").GetComponent<Text>().text = userData.UserName;
        transform.Find("BattleRes/TotalCount").GetComponent<Text>().text = "总场数：" + userData.TotalCount.ToString();
        transform.Find("BattleRes/WinCount").GetComponent<Text>().text = "胜利：" + userData.WinCount.ToString();
    }

    void LoadRoomItem(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab);
            roomItem.transform.SetParent(verticalLayoutGroup.transform);
            roomItem.transform.localScale = Vector3.one;
        }
    }

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
