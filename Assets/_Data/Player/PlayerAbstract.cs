using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbstract : LoadMonoBehaviour
{
    [Header("Player Abstract")]
    protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl => playerCtrl;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerAbstract();
    }

    protected virtual void LoadPlayerAbstract()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GetComponentInParent<PlayerCtrl>();
        //Debug.LogWarning(transform.name + ": LoadPlayerAbstract", gameObject);
    }
}
