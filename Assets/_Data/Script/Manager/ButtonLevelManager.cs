using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLevelManager : LoadMonoBehaviour
{
    private static ButtonLevelManager instance;
    public static ButtonLevelManager Instance => instance;

    [SerializeField] protected List<ButtonLevel> listButtonLevel;

    protected override void Awake()
    {
        base.Awake();
        if (ButtonLevelManager.instance != null) Debug.LogError("Only 1 ButtonLevelManager allow to exist");
        ButtonLevelManager.instance = this;
    }

    public virtual void AddButton(ButtonLevel button)
    {
        this.listButtonLevel.Add(button);
    }

    public void HideAllTickButton()
    {
        foreach (ButtonLevel button in this.listButtonLevel)
        {
            button.HideTickButton();
        }
    }
}
