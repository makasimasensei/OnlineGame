using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour 
{
    protected UIManager uiManager;

    public UIManager UiManager { set => uiManager = value; }

    /// <summary>
    /// Display.
    /// </summary>
    public virtual void OnEnter()
    {

    }

    /// <summary>
    /// Pause.
    /// </summary>
    public virtual void OnPause()
    {

    }

    /// <summary>
    /// Resume.
    /// </summary>
    public virtual void OnResume()
    {

    }

    /// <summary>
    /// Exit.
    /// </summary>
    public virtual void OnExit()
    {

    }
}
