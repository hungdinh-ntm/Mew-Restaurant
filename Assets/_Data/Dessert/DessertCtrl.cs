using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DessertCtrl : KitchenObjCtrl
{
    [Header("Dessert Ctrl")]
    [SerializeField] protected DishSO dishSO;
    public DishSO DishSO => dishSO;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadDishSO();
    }

    protected virtual void LoadDishSO()
    {
        if (this.dishSO != null) return;
        string resPath = "SO/Menu/" + transform.name;
        this.dishSO = Resources.Load<DishSO>(resPath);
    }

    public override void Bin()
    {
        this.DoDespawn();
    }
}
