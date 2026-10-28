using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAbility : FoodAbstract
{
    [Header("Food Ability")]
    [SerializeField] protected bool canCut = false;
    public bool CanCut => canCut;

    [SerializeField] protected FoodSO cutResult;
    public FoodSO CutResult => cutResult;


    [SerializeField] protected bool canDeepFry = false;
    public bool CanDeepFry => canDeepFry;

    [SerializeField] protected FoodSO deepFryResult;
    public FoodSO DeepFryResult => deepFryResult;


    [SerializeField] protected bool canFry = false;
    public bool CanFry => canFry;
    [SerializeField] protected FoodSO fryResult;
    public FoodSO FryResult => fryResult;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCutResult();
        this.LoadDeepFryResult();
        this.LoadFryResult();
    }

    protected virtual void LoadCutResult()
    {
        if (this.cutResult != null) return;
        string resPath = "SO/Foods/" + transform.parent.name + "Slice";
        this.cutResult = Resources.Load<FoodSO>(resPath);
    }

    protected virtual void LoadDeepFryResult()
    {
        if (this.deepFryResult != null) return;
        string resPath = "SO/Foods/" + transform.parent.name + "DeepFry";
        this.deepFryResult = Resources.Load<FoodSO>(resPath);
    }

    protected virtual void LoadFryResult()
    {
        if (this.fryResult != null) return;
        string resPath = "SO/Foods/" + transform.parent.name + "Fry";
        this.fryResult = Resources.Load<FoodSO>(resPath);
    }
}

public interface ICut
{
    void Cut();
}

public interface IFry
{
    void Fry();
}

public interface IDeepFry
{
    void DeepFry();
}
