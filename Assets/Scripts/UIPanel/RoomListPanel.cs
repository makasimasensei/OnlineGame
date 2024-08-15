using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoomListPanel : BasePanel
{
    RectTransform battleRes;
    RectTransform roomList;

     void Start()
    {
        battleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        transform.Find("RoomList").Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
        EnterAnim();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (battleRes != null ) EnterAnim();
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
}
