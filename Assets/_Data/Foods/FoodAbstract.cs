using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAbstract : LoadMonoBehaviour
{
    [Header("Food Abstract")]
    [SerializeField] protected FoodCtrl foodCtrl;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadFoodCtrl();
    }

    protected virtual void LoadFoodCtrl()
    {
        if (this.foodCtrl != null) return;
        this.foodCtrl = transform.parent.GetComponent<FoodCtrl>();
        Debug.LogWarning(transform.name + ": LoadFoodCtrl", gameObject);
    }
}
