using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsObjCtrl : KitchenObjCtrl
{
    [Header("Tools Obj")]
    [SerializeField] protected ContainerSO containerSO;
    public ContainerSO ContainerSO => containerSO;

    public override void Bin()
    {
        //To do
    }
}
