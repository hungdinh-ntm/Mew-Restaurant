using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KitchenObjCtrl : ShootableObjCtrl
{
    [Header("Kitchen Obj")]
    [SerializeField] protected KitchenType kitchenType = KitchenType.Food;
    public KitchenType KitchenType => kitchenType;

    [SerializeField] protected FollowTarget followTarget;
    public FollowTarget FollowTarget => followTarget;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadFollowTarget();
    }

    protected virtual void LoadFollowTarget()
    {
        if (this.followTarget != null) return;
        this.followTarget = transform.GetComponent<FollowTarget>();
    }

    public virtual void SetHolder(Transform holder)
    {
        transform.SetParent(holder);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public override void DoDespawn()
    {
        this.followTarget.SetTarget(null);
        base.DoDespawn();
    }

    public abstract void Bin();
}
