using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootableObjCtrl : LoadMonoBehaviour
{
    [Header("Shootable Obj")]
    [SerializeField] protected Spawner spawner;
    public Spawner Spawner => spawner;

    //[SerializeField] protected Despawn despawn;
    //public Despawn Despawn => despawn;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSpawner();
        //this.LoadDespawn();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = transform.parent.parent.GetComponent<Spawner>();
    }

    //protected virtual void LoadDespawn()
    //{
    //    if (this.despawn != null) return;
    //    this.despawn = GetComponentInChildren<Despawn>();
    //}

    public virtual void DoDespawn()
    {
        this.spawner.Despawn(transform);
    }
}
