using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    Button loginButton;
    Animator animator;

    public override void OnEnter()
    {
        base.OnEnter();
        loginButton = transform.Find("Button").GetComponent<Button>();
        animator = transform.Find("Button").GetComponent<Animator>();
        loginButton.onClick.AddListener(OnLoginClick);
    }

    public void OnLoginClick()
    {
        uiManager.PushPanel(UIPanelType.LogIn);
    }

    public override void OnPause()
    {
        base.OnPause();
        animator.enabled = false;
        loginButton.transform.DOScale(0, 0.2f).OnComplete(() => loginButton.gameObject.SetActive(false));
    }

    public override void OnResume()
    {
        base.OnResume();
        loginButton.gameObject.SetActive(true);
        loginButton.transform.DOScale(1, 0.2f).OnComplete(() => animator.enabled = true);
    }
}
