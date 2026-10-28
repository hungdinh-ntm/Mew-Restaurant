using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DishRecipe", menuName = "SO /DishRecipe")]
public class DishRecipeSO : ScriptableObject
{
    public List<FoodRecipeIngredient> ingredients;
    public DishSO result;

}
