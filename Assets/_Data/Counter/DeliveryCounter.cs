using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    [Header("Delivery Counter")]
    [SerializeField] protected SeatPoint seatPoint;
    public SeatPoint SeatPoint => seatPoint;

    [SerializeField] protected bool canTakeItem = true;

    protected override void Start()
    {
        base.Start();
        SeatPointManager.Instance.AddSeat(this.seatPoint);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSeatPoint();
    }

    protected virtual void LoadSeatPoint()
    {
        if (this.seatPoint != null) return;
        this.seatPoint = GetComponentInChildren<SeatPoint>();
    }

    public override void Interact(PlayerCtrl playerCtrl)
    {
        PlayerHold playerHold = playerCtrl.PlayerHold;

        bool counterHasItem = this.HasItem();
        bool playerHasItem = playerHold.HasItem();
        bool seatPointHasCustomer = this.seatPoint.HasCustomer();

        if (counterHasItem)
        {
            if (!playerHasItem)
            {
                this.TakeDirtyPlate(playerHold);
            }
            return;
        }

        if (!playerHasItem) return;
        if (seatPointHasCustomer) this.GetDish(playerHold);
    }

    protected virtual void GetDish(PlayerHold playerHold)
    {
        Debug.Log("GetDish");
        if (!this.seatPoint.HasCustomer()) return;
        CustomerCtrl customer = this.seatPoint.CurrentCustomer;

        if (customer.CustomerState != CustomerState.WaitDish) return;

        KitchenObjCtrl item = playerHold.CurrentItem;
        if (item.KitchenType == KitchenType.Dessert)
        {
            DessertCtrl dessert = item.GetComponent<DessertCtrl>();
            this.DeliveryDessert(playerHold, customer, dessert);
            return;
        }

        if (item.KitchenType == KitchenType.Plate)
        {
            PlateCtrl plate = item.GetComponent<PlateCtrl>();
            this.DeliveryPlate(playerHold, customer, plate);
            return;
        }
    }

    protected virtual void DeliveryDessert(PlayerHold playerHold, CustomerCtrl customer, DessertCtrl dessert)
    {
        if (dessert == null) return;
        if (!this.CheckCorrectDish(customer, dessert.DishSO)) return;

        KitchenObjCtrl obj = playerHold.RemoveItem();
        this.SetItem(obj);

        this.canTakeItem = false;
        customer.CustomerEat.ReceiveDessert(dessert, this);
    }

    protected virtual void DeliveryPlate(PlayerHold playerHold, CustomerCtrl customer, PlateCtrl plate)
    {
        if (plate == null) return;
        if (plate.PlateState != PlateState.HasDish) return;
        if (!this.CheckCorrectDish(customer, plate.PlateRecipe.CurrentDish)) return;

        KitchenObjCtrl obj = playerHold.RemoveItem();
        this.SetItem(obj);

        this.canTakeItem = false;
        customer.CustomerEat.ReceivePlate(plate, this);
    }

    protected virtual void TakeDirtyPlate(PlayerHold playerHold)
    {
        if (!this.canTakeItem) return;

        KitchenObjCtrl dirtyPlate = this.TakeItem();
        playerHold.HoldItem(dirtyPlate);
        Debug.Log("Take Dirty Plate");
    }

    public virtual void UnlockCounter()
    {
        this.canTakeItem = true;
    }

    protected virtual bool CheckCorrectDish(CustomerCtrl customer, DishSO dish)
    {
        return customer.CustomerOrder.DeliveryOrder(dish);
    }
}
