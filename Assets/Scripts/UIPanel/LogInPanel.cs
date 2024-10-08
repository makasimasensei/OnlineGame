using Common;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogInPanel : BasePanel
{
    Button closeButton;
    TMP_InputField usernameIF;
    TMP_InputField passwordIF;
    Button logInButton;
    Button registerrButton;
    LoginRequest loginRequest;

    void Start()
    {
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);

        usernameIF = transform.Find("LoginPanel/username/InputField (TMP)").GetComponent<TMP_InputField>();
        passwordIF = transform.Find("LoginPanel/password/InputField (TMP)").GetComponent<TMP_InputField>();
        logInButton = transform.Find("Login").GetComponent<Button>();
        registerrButton = transform.Find("SignIn").GetComponent<Button>();
        loginRequest = GetComponent<LoginRequest>();

        logInButton.onClick.AddListener(OnLogInClick);
        registerrButton.onClick.AddListener(OnRegisterClick);
    }

    /// <summary>
    /// Override OnEnter.
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        EnterAnim();
    }

    /// <summary>
    /// Override OnEnter.
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
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Click the close button.
    /// </summary>
    void OnCloseClick()
    {
        PlayClickSound();
        transform.DOScale(0, 0.2f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.2f);
        tweener.OnComplete(() => uiManager.PopPanel());
    }

    /// <summary>
    /// Click the Login button.
    /// </summary>
    void OnLogInClick()
    {
        PlayClickSound();
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空";
        }
        if (msg != "")
        {
            uiManager.ShowMessageSync(msg); return;
        }
        loginRequest.SendRequest(usernameIF.text, passwordIF.text);
    }

    /// <summary>
    /// Click the register button.
    /// </summary>
    void OnRegisterClick()
    {
        PlayClickSound();
        uiManager.PushPanel(UIPanelType.Reg);
    }

    /// <summary>
    /// Handle the returnCode.
    /// </summary>
    /// <param name="returnCode">The code server returns</param>
    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            uiManager.PushPanelSync(UIPanelType.RoomList);
        }
        else
        {
            uiManager.ShowMessageSync("Can't login.Username or password is invalid.");
        }
    }

    /// <summary>
    /// Enter the Animation.
    /// </summary>
    void EnterAnim()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(2.5f, 1);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.2f);
    }

    /// <summary>
    /// Hide the animation.
    /// </summary>
    void HideAnim()
    {
        transform.DOScale(0, 0.2f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.2f);
        tweener.OnComplete(() => gameObject.SetActive(false));
    }
}
