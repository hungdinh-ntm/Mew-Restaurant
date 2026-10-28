using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCtrl : KitchenObjCtrl
{
    [Header("Food Ctrl")]
    [SerializeField] protected FoodSO foodSO;
    public FoodSO FoodSO => foodSO;

    [SerializeField] protected FoodSO defaultSO;

    [SerializeField] protected SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer => spriteRenderer;

    [SerializeField] protected FoodAbility foodAbility;
    public FoodAbility FoodAbility => foodAbility;

    [SerializeField] protected FoodCut foodCut;
    public FoodCut FoodCut => foodCut;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSpriteRenderer();
        this.LoadFoodAbility();
        this.LoadFoodSO();
        this.LoadFoodCut();
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (this.spriteRenderer != null) return;
        this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void LoadFoodAbility()
    {
        if (this.foodAbility != null) return;
        this.foodAbility = GetComponentInChildren<FoodAbility>();
    }

    protected virtual void LoadFoodCut()
    {
        if (this.foodCut != null) return;
        this.foodCut = transform.GetComponentInChildren<FoodCut>();
    }

    protected virtual void LoadFoodSO()
    {
        if (this.foodSO != null) return;
        string resPath = "SO/Foods/" + transform.name;
        this.foodSO = Resources.Load<FoodSO>(resPath);

        this.LoadSprite(this.foodSO);
        this.defaultSO = this.foodSO;
    }

    protected virtual void LoadSprite(FoodSO currentSO)
    {
        this.spriteRenderer.sprite = currentSO.sprite;
    }

    public virtual void SetFoodSO(FoodSO newSO)
    {
        this.foodSO = newSO;
        this.LoadSprite(newSO);
    }

    public virtual void ResetFoodSO()
    {
        this.foodSO = this.defaultSO;
        this.LoadSprite(this.defaultSO);
    }

    public override void DoDespawn()
    {
        this.ResetFoodSO();
        FoodCut foodCut = GetComponentInChildren<FoodCut>();

        if (foodCut != null)
        {
            foodCut.ResetStateFoodCut();
        }
        base.DoDespawn();
    }

    public override void Bin()
    {
        this.DoDespawn();
    }
}
