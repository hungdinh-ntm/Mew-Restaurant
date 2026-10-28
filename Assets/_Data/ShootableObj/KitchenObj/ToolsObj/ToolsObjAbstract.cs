using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ToolsObjAbstract : LoadMonoBehaviour
{
    [Header("Kitchen Abstract")]
    [SerializeField] protected ToolsObjCtrl toolsObjCtrl;
    public ToolsObjCtrl ToolsObjCtrl => toolsObjCtrl;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadToolsObjCtrl();
    }

    protected virtual void LoadToolsObjCtrl()
    {
        if (this.toolsObjCtrl != null) return;
        this.toolsObjCtrl = transform.parent.GetComponent<ToolsObjCtrl>();
    }
}
