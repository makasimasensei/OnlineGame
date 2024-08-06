using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    Text text;
    float remainTime = 1;

    public override void OnEnter()
    {
        base.OnEnter();
        text = GetComponent<Text>();
        text.enabled = false;
        manager.InjectMessagePanel(this);
    }

    public void ShowMessage(string msg)
    {
        text.color = Color.white;
        text.text = msg;
        text.enabled = true;
        Invoke(nameof(Hide), 1);
    }

    void Hide()
    {
        text.DOFade(0, remainTime);
    }
}
