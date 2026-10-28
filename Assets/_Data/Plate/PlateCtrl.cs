using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCtrl : ToolsObjCtrl
{
    [Header("Plate Ctrl")]
    [SerializeField] protected PlateState plateState = PlateState.Empty;
    public PlateState PlateState => plateState;

    [SerializeField] protected PlateRecipe plateRecipe;
    public PlateRecipe PlateRecipe => plateRecipe;

    [SerializeField] protected SpriteRenderer dishRenderer;
    public SpriteRenderer SpriteRenderer => dishRenderer;

    [SerializeField] protected DishSO dirtyDishSO;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlateRecipe();
        this.LoadSpriteRenderer();
    }

    protected virtual void LoadPlateRecipe()
    {
        if (this.plateRecipe != null) return;
        this.plateRecipe = GetComponentInChildren<PlateRecipe>();
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (this.dishRenderer != null) return;
        Transform dish = transform.Find("Dish");
        this.dishRenderer = dish.GetComponent<SpriteRenderer>();
    }

    public virtual void LoadSprite(DishSO dishSO)
    {
        this.dishRenderer.sprite = dishSO.sprite;
    }

    public virtual void LoadSprite(FoodSO foodSO)
    {
        this.dishRenderer.sprite = foodSO.sprite;
    }

    public virtual void SetEmpty()
    {
        this.plateState = PlateState.Empty;
        this.dishRenderer.sprite = null;
        this.PlateRecipe.ClearRecipe();

    }

    public virtual void SetHasDish()
    {
        this.plateState = PlateState.HasDish;

    }

    public virtual void SetDirty()
    {
        this.plateState = PlateState.Dirty;
        this.LoadSprite(this.dirtyDishSO);
    }

    public override void Bin()
    {
        this.SetDirty();
    }
}
