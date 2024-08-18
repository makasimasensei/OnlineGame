using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class MessagePanel : BasePanel
{
    Text text;
    string message;

    private void Update()
    {
        if (message != null)
        {
            ShowMessage(message);
            message = null;
        }
    }

    /// <summary>
    /// Override OnEnter.
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        text = GetComponent<Text>();
        text.enabled = false;
        uiManager.InjectMessagePanel(this);
    }

    /// <summary>
    /// asynchronous show message.
    /// </summary>
    /// <param name="msg">Message needs to be shown.</param>
    public void ShowMessageSync(string msg)
    {
        message = msg;
    }

    /// <summary>
    /// Show message.
    /// </summary>
    /// <param name="msg">Message needs to be shown.</param>
    public void ShowMessage(string msg)
    {
        Color color = text.color;
        color.a = 1;
        text.color = color;
        text.text = msg;
        text.enabled = true;
        Invoke(nameof(Hide), 1);
    }

    /// <summary>
    /// Fade animation.
    /// </summary>
    void Hide()
    {
        text.DOFade(0, 1);
    }
}
