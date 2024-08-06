using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public override void OnEnter()
    {
        base.OnEnter();
        Button loginButton = transform.Find("LogInButton").GetComponent<Button>();
        loginButton.onClick.AddListener(OnLoginClick);
    }

    void OnLoginClick()
    {
        
    }
}
