using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnCtrl : LoadMonoBehaviour
{
    [SerializeField] protected CustomerSpawner customerSpawner;
    public CustomerSpawner CustomerSpawner => customerSpawner;

    [SerializeField] protected CustomerSpawnPoint customerSpawnPoint;
    public CustomerSpawnPoint CustomerSpawnPoint => customerSpawnPoint;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCustomerSpawner();
        this.LoadCustomerSpawnPoint();
    }

    protected virtual void LoadCustomerSpawner()
    {
        if (this.customerSpawner != null) return;
        this.customerSpawner = GetComponent<CustomerSpawner>();
    }

    protected virtual void LoadCustomerSpawnPoint()
    {
        if (this.customerSpawnPoint != null) return;
        this.customerSpawnPoint = GetComponent<CustomerSpawnPoint>();
    }
}
