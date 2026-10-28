using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounter : CounterAbstract
{
    [Header("Base Counter")]
    [SerializeField] protected KitchenObjCtrl itemCounter;
    [SerializeField] protected Transform holdPoint;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadHoldPoint();
    }

    protected virtual void LoadHoldPoint()
    {
        if (this.holdPoint != null) return;
        this.holdPoint = transform.Find("HoldPoint");
    }

    public virtual void SetItem(KitchenObjCtrl item)
    {
        this.itemCounter = item;
        item.FollowTarget.SetTarget(this.holdPoint);
    }

    public virtual KitchenObjCtrl TakeItem()
    {
        KitchenObjCtrl item = this.itemCounter;
        this.ClearItem();
        return item;
    }

    public virtual bool HasItem()
    {
        return this.itemCounter != null;
    }

    public virtual KitchenObjCtrl GetItem()
    {
        return this.itemCounter;
    }

    public virtual void ClearItem()
    {
        this.itemCounter = null;
    }

    protected virtual void SwapItem(PlayerHold playerHold)
    {
        KitchenObjCtrl playerItem = playerHold.RemoveItem();
        KitchenObjCtrl counterItem = this.TakeItem();

        playerHold.HoldItem(counterItem);
        this.SetItem(playerItem);
        Debug.Log("Swap Item");
    }
}
