using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomerAbstract : LoadMonoBehaviour
{
    [Header("Customer Abstract")]
    [SerializeField] protected CustomerCtrl customerCtrl;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCustomerCtrl();
    }

    protected virtual void LoadCustomerCtrl()
    {
        if (this.customerCtrl != null) return;
        this.customerCtrl = transform.parent.GetComponent<CustomerCtrl>();
    }
}
