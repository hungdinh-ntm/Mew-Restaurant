using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonCtrl : LoadMonoBehaviour
{
    [SerializeField] protected Transform panel;
    public Transform Panel => panel;

    [SerializeField] protected ButtonLevelSpawner buttonSpawner;
    public ButtonLevelSpawner ButtonSpawner => buttonSpawner;

    protected override void LoadComponent()
    {
        this.LoadPanel();
        this.LoadLevelButtonSpawner();
    }

    protected virtual void LoadPanel()
    {
        if (this.panel != null) return;
        this.panel = transform.Find("LevelPanel");
    }

    protected virtual void LoadLevelButtonSpawner()
    {
        if (this.buttonSpawner != null) return;
        this.buttonSpawner = GetComponentInChildren<ButtonLevelSpawner>();
    }
}
