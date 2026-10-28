using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatPoint : LoadMonoBehaviour
{
    [Header("Seat Point")]
    [SerializeField] protected CustomerCtrl currentCustomer;
    public CustomerCtrl CurrentCustomer => currentCustomer;

    [SerializeField] protected SeatDirection faceDirection = SeatDirection.Down;
    public SeatDirection FaceDirection => faceDirection;

    public virtual void SetCustomer(CustomerCtrl customer)
    {
        this.currentCustomer = customer;
    }

    public virtual void ClearCustomer()
    {
        this.currentCustomer = null;
    }

    public virtual bool HasCustomer()
    {
        return this.currentCustomer != null;
    }
}
