using Common;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class RegisterPanel : BasePanel
{
    TMP_InputField username_IF;
    TMP_InputField password_IF;
    TMP_InputField repassword_IF;

    RegisterRequest register_request;

    private void Start()
    {
        username_IF = transform.Find("SignInPanel/username/InputField (TMP)").GetComponent<TMP_InputField>();
        password_IF = transform.Find("SignInPanel/password1/InputField (TMP)").GetComponent<TMP_InputField>();
        repassword_IF = transform.Find("SignInPanel/password2/InputField (TMP)").GetComponent<TMP_InputField>();

        register_request = GetComponent<RegisterRequest>();

        transform.Find("SignIn").GetComponent<Button>().onClick.AddListener(OnRegisterClick);
        transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
    }

    /// <summary>
    ///  Override OnEnter.
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(2.5f, 1);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.2f);
    }

    /// <summary>
    /// Click register button.
    /// </summary>
    void OnRegisterClick()
    {
        PlayClickSound();
        string s = "";
        if (string.IsNullOrEmpty(username_IF.text))
        {
            s += "Username can't be null.";
        }
        if (string.IsNullOrEmpty(password_IF.text) || string.IsNullOrEmpty(repassword_IF.text))
        {
            s += "Password can't be null.";
        }
        if (password_IF.text != repassword_IF.text)
        {
            s += "Password inconsistency.";
        }
        if (s != "")
        {
            uiManager.ShowMessageSync(s); return;
        }
        else
        {
            register_request.SendRequest(username_IF.text, password_IF.text);
        }
    }

    /// <summary>
    /// Handle the register event.
    /// </summary>
    /// <param name="returnCode">Code of return.</param>
    public void OnRegisterResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            uiManager.ShowMessageSync("Register success.");
        }
        else
        {
            uiManager.ShowMessageSync("Register success.");
        }
    }

    /// <summary>
    /// Click close button.
    /// </summary>
    void OnCloseClick()
    {
        PlayClickSound();
        transform.DOScale(0, 0.2f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.2f);
        tweener.OnComplete(() => uiManager.PopPanel());
    }

    /// <summary>
    /// Override
    /// </summary>
    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }
}
