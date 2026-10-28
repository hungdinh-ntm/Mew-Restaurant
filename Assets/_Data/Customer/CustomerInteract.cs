using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerInteract : CustomerAbstract, IInteractable
{
    public void Interact(PlayerCtrl playerCtrl)
    {
        if (this.customerCtrl.CustomerState != CustomerState.WaitOrder) return;
        this.customerCtrl.CustomerOrder.WaitDish();
    }
}
