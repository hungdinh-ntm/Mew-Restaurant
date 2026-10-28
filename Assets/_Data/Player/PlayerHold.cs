using UnityEngine;

public class PlayerHold : LoadMonoBehaviour
{
    [Header("Player Hold")]
    [SerializeField] protected KitchenObjCtrl currentItem;
    public KitchenObjCtrl CurrentItem => currentItem;

    [SerializeField] protected Transform holdPoint;
    public Transform HoldPoint => holdPoint;

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

    public void HoldItem(KitchenObjCtrl item)
    {
        if (this.HasItem()) return;

        this.currentItem = item;
        item.FollowTarget.SetTarget(this.holdPoint);
    }

    public virtual KitchenObjCtrl RemoveItem()
    {
        KitchenObjCtrl item = this.currentItem;
        this.currentItem = null;
        return item;
    }

    public virtual bool HasItem()
    {
        return this.currentItem != null;
    }
}
