using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move1 : FollowTarget
{
    [Header("Customer Movement")]
    [SerializeField] protected Transform exitPoint;
    [SerializeField] protected float distance;

    protected override void LoadComponent()
    {
        this.LoadExitPoint();
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.speed = 2f;
    }

    protected override void Following()
    {
        if (GameManager.Instance.GameOver) return;
        if (this.target == null) return;
        transform.parent.position = Vector3.Lerp(transform.parent.position, this.target.position, Time.fixedDeltaTime * this.speed);

        this.CheckReachedTarget();
    }

    protected virtual void LoadExitPoint()
    {
        Debug.Log("1");
        if (this.exitPoint != null) return;
        CustomerCtrl customer = transform.parent.GetComponent<CustomerCtrl>();

        this.exitPoint = customer.transform.Find("Point");
        Debug.Log("2");
    }

    protected virtual void CheckReachedTarget()
    {
        if (this.target == null) return;

        this.distance = Vector2.Distance(transform.parent.position, this.target.position);

        if (this.distance > 0.001f) return;

        CustomerCtrl customerCtrl = transform.parent.GetComponent<CustomerCtrl>();

        if (customerCtrl.CustomerState == CustomerState.Moving)
        {
            this.ReachedSeat(customerCtrl);
            return;
        }

        if (customerCtrl.CustomerState == CustomerState.Leaving)
        {
            this.ReachedExit(customerCtrl);
            return;
        }
    }

    protected virtual void ReachedSeat(CustomerCtrl customerCtrl)
    {
        customerCtrl.SetState(CustomerState.WaitOrder);
        customerCtrl.CustomerOrder.CallStaff();
        this.target = null;
        Debug.Log("Customer Reached Seat");
    }

    protected virtual void ReachedExit(CustomerCtrl customerCtrl)
    {
        customerCtrl.Spawner.Despawn(customerCtrl.transform);
        customerCtrl.ResetCustomer();
        this.target = null;
        Debug.Log("Customer Leave Restaurant");
    }

    public virtual void MoveExit()
    {
        if (GameManager.Instance.GameOver) return;
        this.target = this.exitPoint;
    }
}
