using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonList : LoadMonoBehaviour
{
    [SerializeField] protected UIButtonCtrl uiButtonCtrl;

    protected override void Start()
    {
        this.ShowButtons();
    }

    protected override void LoadComponent()
    {
        this.LoadUIButtonCtrl();
    }

    protected virtual void LoadUIButtonCtrl()
    {
        if (this.uiButtonCtrl != null) return;
        this.uiButtonCtrl = transform.parent.GetComponent<UIButtonCtrl>();
    }

    protected virtual void ShowButtons()
    {
        List<LevelSO> list = LevelManager.Instance.ListLevels;
        ButtonLevelSpawner spawner = this.uiButtonCtrl.ButtonSpawner;

        foreach (LevelSO levelSO in list)
        {
            spawner.SpawnButton(levelSO);
        }

    }
}

