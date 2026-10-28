using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCtrl : ShootableObjCtrl
{
    [Header("Customer")]
    [SerializeField] protected CustomerState customerState;
    public CustomerState CustomerState => customerState;

    [SerializeField] protected SeatPoint currentSeat;
    public SeatPoint CurrentSeat => currentSeat;

    [SerializeField] protected CustomerMove customerMovement;
    public CustomerMove CustomerMovement => customerMovement;

    [SerializeField] protected CustomerOrder customerOrder;
    public CustomerOrder CustomerOrder => customerOrder;

    [SerializeField] protected CustomerEat customerEat;
    public CustomerEat CustomerEat => customerEat;

    [Header("UI")]
    [SerializeField] protected UIBubble uiBubble;
    public UIBubble UIBubble => uiBubble;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCustomerMovement();
        this.LoadCustomerOrder();
        this.LoadCustomerEat();
    }

    protected virtual void LoadCustomerMovement()
    {
        if (this.customerMovement != null) return;
        this.customerMovement = GetComponentInChildren<CustomerMove>();
    }

    protected virtual void LoadCustomerOrder()
    {
        if (this.customerOrder != null) return;
        this.customerOrder = GetComponentInChildren<CustomerOrder>();
    }

    protected virtual void LoadCustomerEat()
    {
        if (this.customerEat != null) return;
        this.customerEat = GetComponentInChildren<CustomerEat>();
    }

    public virtual void SetSeat(SeatPoint seat)
    {
        this.currentSeat = seat;
        this.CustomerMovement.SetTarget(seat.transform);
    }

    public virtual void SetState(CustomerState state)
    {
        this.customerState = state;
    }

    public virtual void LeaveRestaurant()
    {
        this.SetState(CustomerState.Leaving);

        if (this.currentSeat != null)
        {
            this.currentSeat.ClearCustomer();
            this.currentSeat = null;
        }
        this.CustomerMovement.MoveExit();

        if (this.uiBubble != null)
        {
            UIBubbleSpawner.Instance.DespawnUIBubble(this.uiBubble);
            this.uiBubble = null;
        }
    }

    public virtual void ResetCustomer()
    {
        this.SetState(CustomerState.Moving);
        this.customerOrder.ClearOrder();
    }

    public void SetBubble(UIBubble bubble)
    {
        this.uiBubble = bubble;
        this.uiBubble.SetCustomer(this);
    }

    public virtual void AssignSeat()
    {
        if (this.customerState != CustomerState.Moving) return;
        SeatPoint seat = SeatPointManager.Instance.FindSeat();

        if (seat == null) return;

        seat.SetCustomer(this);
        this.SetSeat(seat);
    }
}
