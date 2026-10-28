using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : LoadMonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected Transform target;
    [SerializeField] protected Transform exitPoint;
    [SerializeField] protected CustomerAnimation customerAnimation;

    [SerializeField] protected float speed = 2f;
    [SerializeField] protected float distance;

    //protected override void LoadComponent()
    //{
    //    base.LoadComponent();
    //    this.LoadExitPoint();
    //}

    protected virtual void FixedUpdate()
    {
        this.MoveToTarget();
    }

    //protected virtual void LoadExitPoint()
    //{
    //    if (this.exitPoint != null) return;

    //    this.exitPoint = transform.parent.Find("Point");
    //}

    protected virtual void MoveToTarget()
    {
        //if (GameManager.Instance.GameOver) return;
        if (target == null) return;

        Vector3 currentPos = transform.parent.position;
        Vector3 targetPos = target.position;

        Vector2 dir = Vector2.zero;

        if (Mathf.Abs(targetPos.x - currentPos.x) > 0.02f)
        {
            dir.x = Mathf.Sign(targetPos.x - currentPos.x);
        }
        else if (Mathf.Abs(targetPos.y - currentPos.y) > 0.02f)
        {
            dir.y = Mathf.Sign(targetPos.y - currentPos.y);
        }

        transform.parent.position +=
            (Vector3)dir * speed * Time.fixedDeltaTime;

       customerAnimation.UpdateAnimation(dir);

        this.CheckReachedTarget();
    }

    protected virtual void CheckReachedTarget()
    {
        if (target == null) return;

        distance = Vector2.Distance(transform.parent.position, target.position);

        if (distance > 0.05f) return;

        transform.parent.position = target.position;

        customerAnimation.UpdateAnimation(Vector2.zero);

        //switch (customerCtrl.CustomerState)
        //{
        //    case CustomerState.Moving:
        //        ReachedSeat();
        //        break;

        //    case CustomerState.Leaving:
        //        ReachedExit();
        //        break;
        //}
    }

    //protected virtual void ReachedSeat()
    //{
    //    customerCtrl.SetState(CustomerState.WaitOrder);

    //    customerCtrl.CustomerOrder.CallStaff();

    //    target = null;

    //    Debug.Log("Customer Reached Seat");
    //}

    //protected virtual void ReachedExit()
    //{
    //    customerCtrl.Spawner.Despawn(customerCtrl.transform);

    //    customerCtrl.ResetCustomer();

    //    target = null;

    //    Debug.Log("Customer Leave Restaurant");
    //}

    //public virtual void SetTarget(Transform target)
    //{
    //    this.target = target;
    //}

    //public virtual void MoveExit()
    //{
    //    if (GameManager.Instance.GameOver) return;

    //    this.target = this.exitPoint;
    //}
}
