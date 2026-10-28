using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCut : FoodAbstract, ICut
{
    [Header("Food Cut")]
    [SerializeField] protected bool isAlreadyCut = false;
    public bool IsAlreadyCut => isAlreadyCut;

    public virtual bool CheckCanCut()
    {
        return !isAlreadyCut;
    }

    public virtual void Cut()
    {
        Debug.Log("Cut");
        if (!this.foodCtrl.FoodAbility.CanCut) return;
        if (!this.CheckCanCut()) return;

        this.isAlreadyCut = true;

        FoodSO nextSO = this.foodCtrl.FoodAbility.CutResult;
        if (nextSO == null) return;
        this.foodCtrl.SetFoodSO(nextSO);
    }

    public virtual void ResetStateFoodCut()
    {
        this.isAlreadyCut = false;
    }
}
