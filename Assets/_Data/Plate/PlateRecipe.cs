using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateRecipe : ToolsObjAbstract
{
    [Header("Plate Recipe")]
    [SerializeField] protected List<FoodSO> ingredients = new();
    [SerializeField] protected List<DishRecipeSO> recipes;

    [SerializeField] protected DishSO currentDish;
    public DishSO CurrentDish => currentDish;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRecipes();
    }

    protected virtual void LoadRecipes()
    {
        if (this.recipes.Count > 0) return;
        this.recipes = Resources.LoadAll<DishRecipeSO>("SO/Ingredient").ToList();
    }

    public virtual bool PlateAddItem(FoodCtrl food)
    {
        if (!this.CheckItem(food)) return false;
        this.AddIngredient(food);
        this.CheckRecipe();
        return true;
    }

    protected virtual bool CheckItem(FoodCtrl food)
    {
        List<FoodSO> listAllow = toolsObjCtrl.ContainerSO.allowedFoods;
        if (food.FoodSO == null) return false;
        if (!listAllow.Contains(food.FoodSO)) return false;
        if (ingredients.Contains(food.FoodSO)) return false;
        return true;
    }

    protected virtual void AddIngredient(FoodCtrl food)
    {
        this.ingredients.Add(food.FoodSO);

        if (this.ingredients.Count <= 1)
        {
            PlateCtrl plateCtrl = this.toolsObjCtrl.GetComponent<PlateCtrl>();
            plateCtrl.LoadSprite(food.FoodSO);
        }
    }

    protected virtual void CheckRecipe()
    {
        foreach (DishRecipeSO recipe in this.recipes)
        {
            bool isMatch = true;
            if (recipe.ingredients.Count != this.ingredients.Count) continue;

            foreach (FoodRecipeIngredient x in recipe.ingredients)
            {
                if (!this.ingredients.Contains(x.foodSO))
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
            {
                this.currentDish = recipe.result;
                this.UpdateDishVisual();

                PlateCtrl plateCtrl = this.toolsObjCtrl.GetComponent<PlateCtrl>();
                plateCtrl.SetHasDish();

                Debug.Log("Current Dish: " + this.currentDish.dishName);
                return;
            }
        }
        this.currentDish = null;
    }

    protected virtual void UpdateDishVisual()
    {
        PlateCtrl plateCtrl = this.toolsObjCtrl.GetComponent<PlateCtrl>();
        plateCtrl.LoadSprite(this.currentDish);
    }

    public virtual void ClearRecipe()
    {
        this.ingredients.Clear();
        this.currentDish = null;
    }
}
