using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEat : CustomerAbstract
{
    [Header("Customer Eat")]
    [SerializeField] protected float eatTime = 2f;

    [SerializeField] protected DeliveryCounter currentCounter;

    public virtual void ReceivePlate(PlateCtrl plate, DeliveryCounter counter)
    {
        this.currentCounter = counter;
        this.customerCtrl.SetState(CustomerState.Eating);
        this.customerCtrl.CustomerOrder.PauseTimer();

        StartCoroutine(this.EatRoutine(plate));
    }

    public virtual void ReceiveDessert(DessertCtrl dessert, DeliveryCounter counter)
    {
        this.currentCounter = counter;
        this.customerCtrl.SetState(CustomerState.Eating);
        this.customerCtrl.CustomerOrder.PauseTimer();

        StartCoroutine(this.DrinkRoutine(dessert));
    }

    protected virtual IEnumerator EatRoutine(PlateCtrl plate)
    {
        yield return new WaitForSeconds(this.eatTime);

        if (this.customerCtrl == null)
        {
            this.currentCounter.UnlockCounter();
            yield break;
        }

        if (this.customerCtrl.CustomerState == CustomerState.Leaving)
        {
            this.currentCounter.UnlockCounter();
            yield break;
        }

        plate.SetDirty();
        this.currentCounter.UnlockCounter();
        this.customerCtrl.CustomerOrder.RemoveDish(plate.PlateRecipe.CurrentDish);

        this.FinishEating();
    }

    protected virtual IEnumerator DrinkRoutine(DessertCtrl dessert)
    {
        yield return new WaitForSeconds(this.eatTime);

        if (this.customerCtrl == null)
        {
            this.currentCounter.UnlockCounter();
            yield break;
        }

        if (this.customerCtrl.CustomerState == CustomerState.Leaving)
        {
            this.currentCounter.UnlockCounter();
            yield break;
        }

        dessert.DoDespawn();

        this.currentCounter.ClearItem();
        this.currentCounter.UnlockCounter();
        this.customerCtrl.CustomerOrder.RemoveDish(dessert.DishSO);

        this.FinishEating();
    }

    protected virtual void FinishEating()
    {
        if (this.customerCtrl.CustomerOrder.HasFinishedEating())
        {
            return;
        }

        this.customerCtrl.SetState(CustomerState.WaitDish);
        this.customerCtrl.CustomerOrder.ResumeTimer();
    }
}
