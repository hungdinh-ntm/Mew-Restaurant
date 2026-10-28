using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBubbleSpawner : Spawner
{
    [Header("UI Bubble Spawner")]
    private static UIBubbleSpawner instance;
    public static UIBubbleSpawner Instance => instance;

    public static string UIBubble = "UIBubble";

    protected override void Awake()
    {
        base.Awake();
        if (UIBubbleSpawner.instance != null) Debug.LogError("Only 1 UIItemSpawner allow to exist");
        UIBubbleSpawner.instance = this;
    }

    public virtual void DespawnUIBubble(UIBubble uiBubble)
    {
        uiBubble.ResetBubble();
        this.Despawn(uiBubble.transform);
    }
}
