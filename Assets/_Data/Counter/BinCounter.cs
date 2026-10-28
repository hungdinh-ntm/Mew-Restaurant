using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinCounter : CounterAbstract
{
    public override void Interact(PlayerCtrl playerCtrl)
    {
        PlayerHold playerHold = playerCtrl.PlayerHold;

        if (!playerHold.HasItem()) return;
        KitchenObjCtrl obj = playerHold.CurrentItem.GetComponent<KitchenObjCtrl>();

        if(obj.KitchenType!= KitchenType.Plate) playerHold.RemoveItem();
        obj.Bin();
    }
}
