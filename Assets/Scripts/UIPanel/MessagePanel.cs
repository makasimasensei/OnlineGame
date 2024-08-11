using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class MessagePanel : BasePanel
{
    Text text;
    float remainTime = 1;
    string message;

    public override void OnEnter()
    {
        base.OnEnter();
        text = GetComponent<Text>();
        text.enabled = false;
        uiManager.InjectMessagePanel(this);
    }

    private void Update()
    {
        if (message != null)
        {
            ShowMessage(message);
            message = null;
        }
    }

    public void ShowMessageSync(string msg)
    {
        message = msg;
    }

    public void ShowMessage(string msg)
    {
        Color color = text.color;
        color.a = 1;
        text.color = color;
        text.text = msg;
        text.enabled = true;
        Invoke(nameof(Hide), 1);
    }

    void Hide()
    {
        text.DOFade(0, remainTime);
    }
}
