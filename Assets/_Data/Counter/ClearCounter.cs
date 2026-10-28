using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(PlayerCtrl playerCtrl)
    {
        PlayerHold playerHold = playerCtrl.PlayerHold;

        bool playerHasItem = playerHold.HasItem();
        bool counterHasItem = this.HasItem();

        if (!playerHasItem && counterHasItem)
        {
            this.TakeItemFromCounter(playerHold);
            return;
        }

        if (playerHasItem && !counterHasItem)
        {
            this.PlaceItemOnCounter(playerHold);
            return;
        }

        if (playerHasItem && counterHasItem)
        {
            this.HandleBothHaveItem(playerHold);
        }
    }

    protected virtual void HandleBothHaveItem(PlayerHold playerHold)
    {
        KitchenObjCtrl playerItem = playerHold.CurrentItem;
        KitchenObjCtrl counterItem = this.GetItem();

        bool canCombine = this.CanCombineItem(playerItem, counterItem);

        if (canCombine)
        {
            this.AddFoodToPlate(playerHold, playerItem, counterItem);
            return;
        }
        this.SwapItem(playerHold);
    }

    protected virtual void TakeItemFromCounter(PlayerHold playerHold)
    {
        KitchenObjCtrl item = this.TakeItem();
        playerHold.HoldItem(item);
        Debug.Log("Take Item From Counter");
    }

    protected virtual void PlaceItemOnCounter(PlayerHold playerHold)
    {
        KitchenObjCtrl item = playerHold.RemoveItem();
        this.SetItem(item);
        Debug.Log("Place Item On Counter");
    }

    protected virtual bool CanCombineItem(KitchenObjCtrl playerItem, KitchenObjCtrl counterItem)
    {
        bool playerPlate = playerItem.KitchenType == KitchenType.Plate;
        bool counterPlate = counterItem.KitchenType == KitchenType.Plate;

        bool playerFood = playerItem.KitchenType == KitchenType.Food;
        bool counterFood = counterItem.KitchenType == KitchenType.Food;

        bool playerCanCombine = playerPlate && counterFood;
        bool counterCanCombine = counterPlate && playerFood;
        return playerCanCombine || counterCanCombine;
    }

    protected virtual void AddFoodToPlate(PlayerHold playerHold, KitchenObjCtrl playerItem, KitchenObjCtrl counterItem)
    {
        if (playerItem.KitchenType == KitchenType.Plate)
        {
            this.AddCounterFoodToPlayerPlate(playerHold);
            return;
        }

        if (counterItem.KitchenType == KitchenType.Plate)
        {
            this.AddPlayerFoodToCounterPlate(playerHold);
        }
    }

    protected virtual void AddCounterFoodToPlayerPlate(PlayerHold playerHold)
    {
        PlateCtrl plate = playerHold.CurrentItem.GetComponent<PlateCtrl>();
        FoodCtrl food = this.GetItem().GetComponent<FoodCtrl>();

        bool success = plate.PlateRecipe.PlateAddItem(food);
        if (!success)
        {
            this.SwapItem(playerHold);
            return;
        }

        FoodCtrl foodDespawn = this.TakeItem().GetComponent<FoodCtrl>();
        foodDespawn.DoDespawn();
        Debug.Log("Add Counter Food To Player Plate");
    }

    protected virtual void AddPlayerFoodToCounterPlate(PlayerHold playerHold)
    {
        PlateCtrl plate = this.GetItem().GetComponent<PlateCtrl>();
        FoodCtrl food = playerHold.CurrentItem.GetComponent<FoodCtrl>();

        bool success = plate.PlateRecipe.PlateAddItem(food);
        if (!success)
        {
            this.SwapItem(playerHold);
            return;
        }

        FoodCtrl foodDespawn = playerHold.RemoveItem().GetComponent<FoodCtrl>();
        foodDespawn.DoDespawn();
        Debug.Log("Add Player Food To Counter Plate");
    }
}
