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

    UserData userData = null;
    UserData userData1 = null;
    UserData userData2 = null;

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

        bluePanel = transform.Find("BluePanel").GetComponent<RectTransform>();
        redPanel = transform.Find("RedPanel").GetComponent<RectTransform>();

        EnterAnim();
    }

    private void Update()
    {
        if (userData != null)
        {
            SetLocalPlayerRes(userData.UserName, userData.TotalCount.ToString(), userData.WinCount.ToString());
            ClearEnemyPlayerRes();
            userData = null;
        }
        if (userData1 != null || userData2 != null)
        {
            SetLocalPlayerRes(userData1.UserName, userData1.TotalCount.ToString(), userData1.WinCount.ToString());
            SetEnemyPlayerRes(userData1.UserName, userData1.TotalCount.ToString(), userData1.WinCount.ToString());
        }
        userData1 = null;
        userData2 = null;
    }

    /// <summary>
    /// Override OnEnter.
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        if (bluePanel != null) EnterAnim();
    }

    /// <summary>
    /// Override OnEnter.
    /// </summary>
    public override void OnExit()
    {
        base.OnExit();
        ExitAnim();
    }

    /// <summary>
    /// Override OnPause.
    /// </summary>
    public override void OnPause()
    {
        base.OnPause();
        ExitAnim();
    }

    /// <summary>
    /// Override OnResume.
    /// </summary>
    public override void OnResume()
    {
        base.OnResume();
        EnterAnim();
    }

    public void SetLocalPlayerResSync()
    {
        userData = gameFacade.GetUserData();
    }

    public void SetAllPlayerResSync(UserData ud1, UserData ud2)
    {
        this.userData1 = ud1;
        this.userData2 = ud2;
    }

    /// <summary>
    /// Set players of blue side.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="totalcount"></param>
    /// <param name="wincount"></param>
    public void SetLocalPlayerRes(string username, string totalcount, string wincount)
    {
        localPlayerUsername.text = username;
        localPlayerTotalCount.text = "总场数：" + totalcount;
        localPlayerWinCount.text = "胜利：" + wincount;
    }

    /// <summary>
    /// Set players of red side.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="totalcount"></param>
    /// <param name="wincount"></param>
    public void SetEnemyPlayerRes(string username, string totalcount, string wincount)
    {
        enemyPlayerUsername.text = username;
        enemyTotalCount.text = "总场数：" + totalcount;
        enemyWinCount.text = "胜利：" + wincount;
    }

    /// <summary>
    /// Clear the players' setting.
    /// </summary>
    public void ClearEnemyPlayerRes()
    {
        enemyPlayerUsername.text = "";
        enemyTotalCount.text = "等待玩家加入...";
        enemyWinCount.text = "";
    }

    /// <summary>
    /// Click the start button.
    /// </summary>
    void OnStartClick()
    {

    }

    /// <summary>
    /// Click the close button.
    /// </summary>
    void OnExitClick()
    {

    }

    /// <summary>
    /// Enter the animation.
    /// </summary>
    void EnterAnim()
    {
        gameObject.SetActive(true);
        bluePanel.localPosition = new Vector3(-1000, 0, 0);
        bluePanel.DOLocalMoveX(-350, 0.4f);

        redPanel.localPosition = new Vector3(1000, 0, 0);
        redPanel.DOLocalMoveX(350, 0.4f);
    }

    /// <summary>
    /// Exit the animation.
    /// </summary>
    void ExitAnim()
    {
        bluePanel.DOLocalMoveX(-1000, 0.4f);
        redPanel.DOLocalMoveX(1000, 0.4f).OnComplete(() => gameObject.SetActive(false));
    }
}
