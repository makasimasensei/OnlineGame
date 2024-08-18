using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : BasePanel
{
    Text localPlayerUsername;
    Text localPlayerTotalCount;
    Text localPlayerWinCount;

    Text enemyPlayerUsername;
    Text enemyTotalCount;
    Text enemyWinCount;

    RectTransform bluePanel;
    RectTransform redPanel;

    private void Start()
    {
        localPlayerUsername = transform.Find("BluePanel/Username").GetComponent<Text>();
        localPlayerTotalCount = transform.Find("BluePanel/TotalCount").GetComponent<Text>();
        localPlayerWinCount = transform.Find("BluePanel/WinCount").GetComponent<Text>();

        enemyPlayerUsername = transform.Find("RedPanel/Username").GetComponent<Text>();
        enemyTotalCount = transform.Find("RedPanel/TotalCount").GetComponent<Text>();
        enemyWinCount = transform.Find("RedPanel/WinCount").GetComponent<Text>();

        transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(OnStartClick);
        transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnExitClick);

        bluePanel.transform.Find("BluePanel");
        redPanel.transform.Find("RedPanel");
        EnterAnim();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (bluePanel != null) EnterAnim();
    }

    public override void OnExit()
    {
        base.OnExit();
        ExitAnim();
    }

    public override void OnPause()
    {
        base.OnPause();
        ExitAnim();
    }

    public override void OnResume()
    {
        base.OnResume();
        EnterAnim();
    }

    void SetLocalPlayerRes(string username, string totalcount, string wincount)
    {
        localPlayerUsername.text = username;
        localPlayerTotalCount.text = "总场数：" + totalcount;
        localPlayerWinCount.text = "胜利：" + wincount;
    }

    void SetEnemyPlayerRes(string username, string totalcount, string wincount)
    {
        enemyPlayerUsername.text = username;
        enemyTotalCount.text = "总场数：" + totalcount;
        enemyWinCount.text = "胜利：" + wincount;
    }

    void ClearEnemyPlayerRes()
    {
        enemyPlayerUsername.text = "";
        enemyTotalCount.text = "";
        enemyWinCount.text = "";
    }

    void OnStartClick()
    {

    }

    void OnExitClick()
    {

    }

    void EnterAnim()
    {
        gameObject.SetActive(true);
        bluePanel.localPosition = new Vector3(-1000, 0, 0);
        bluePanel.DOLocalMoveX(-350, 0.4f);

        redPanel.localPosition = new Vector3(1000, 0, 0);
        redPanel.DOLocalMoveX(350, 0.4f);
    }

    void ExitAnim()
    {
        bluePanel.DOLocalMoveX(-1000, 0.4f);
        redPanel.DOLocalMoveX(1000, 0.4f).OnComplete(() => gameObject.SetActive(false));
    }
}
