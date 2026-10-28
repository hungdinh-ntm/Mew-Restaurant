using Pathfinding;
using System.Collections;
using UnityEngine;

public class CustomerMove : CustomerAbstract
{
    [Header("Component")]
    [SerializeField] protected Seeker seeker;
    [SerializeField] protected CustomerAnimation cusAnimation;

    [Header("Target")]
    [SerializeField] protected Transform target;
    [SerializeField] protected Transform exitPoint;

    [Header("Move")]
    [SerializeField] protected float speed = 2f;
    [SerializeField] protected float limitDistance = 0.2f;

    protected Path path;
    protected int currentWaypoint;

    protected void FixedUpdate()
    {
        this.MoveAlongPath();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadAnimator();
        this.LoadSeeker();
        this.LoadExitPoint();
    }

    protected virtual void LoadSeeker()
    {
        if (this.seeker != null) return;
        this.seeker = GetComponent<Seeker>();
    }

    protected virtual void LoadAnimator()
    {
        if (this.cusAnimation != null) return;
        this.cusAnimation = GetComponent<CustomerAnimation>();
    }

    protected virtual void LoadExitPoint()
    {
        if (this.exitPoint != null) return;
        this.exitPoint = this.customerCtrl.transform.parent.parent.Find("Point");
    }

    protected virtual void CalculatePath()
    {
        if (this.target == null) return;
        if (!this.seeker.IsDone()) return;

        this.seeker.StartPath(customerCtrl.transform.position, this.target.position, OnPathComplete);
    }

    protected virtual void OnPathComplete(Path p)
    {
        if (p.error) return;

        this.path = p;
        this.currentWaypoint = 0;
    }

    protected virtual void MoveAlongPath()
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameOver) return;

        if (this.path == null) return;

        if (this.currentWaypoint >= this.path.vectorPath.Count)
        {
            cusAnimation.UpdateAnimation(Vector2.zero);
            CheckReachedTarget();
            return;
        }

        Vector2 currentPos = customerCtrl.transform.position;
        Vector2 nextPos = this.path.vectorPath[this.currentWaypoint];

        Vector2 direction = nextPos - currentPos;

        if (direction.magnitude <= this.limitDistance)
        {
            this.currentWaypoint++;
            return;
        }

        direction.Normalize();

        customerCtrl.transform.position += (Vector3)(direction * this.speed * Time.fixedDeltaTime);
        cusAnimation.UpdateAnimation(direction);
    }

    protected virtual void CheckReachedTarget()
    {
        if (this.target == null) return;
        float distance = Vector2.Distance(customerCtrl.transform.position, this.target.position);
        Debug.Log("Distan-target: " + distance);

        if (distance > this.limitDistance) return;
        customerCtrl.transform.position = this.target.position;
        Debug.Log("OKay");

        CancelInvoke(nameof(CalculatePath));
        this.path = null;
        cusAnimation.UpdateAnimation(Vector2.zero);

        switch (customerCtrl.CustomerState)
        {
            case CustomerState.Moving:
                ReachedSeat();
                break;

            case CustomerState.Leaving:
                ReachedExit();
                break;
        }
    }

    protected virtual void ReachedSeat()
    {
        customerCtrl.SetState(CustomerState.WaitOrder);
        cusAnimation.FaceDirection(this.customerCtrl.CurrentSeat.FaceDirection);
        customerCtrl.CustomerOrder.CallStaff();

        this.target = null;
        Debug.Log("Customer Reached Seat");
    }

    protected virtual void ReachedExit()
    {
        customerCtrl.Spawner.Despawn(customerCtrl.transform);
        customerCtrl.ResetCustomer();

        this.target = null;
        Debug.Log("Customer Leave");
    }

    public virtual void SetTarget(Transform newTarget)
    {
        if (GameManager.Instance.GameOver) return;

        this.target = newTarget;
        CancelInvoke(nameof(CalculatePath));
        this.CalculatePath();
    }

    public virtual void MoveExit()
    {
        this.SetTarget(exitPoint);
    }
}