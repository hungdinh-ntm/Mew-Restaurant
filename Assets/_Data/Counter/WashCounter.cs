using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashCounter : BaseCounter
{
    [SerializeField] protected float washTimeMax = 3f;
    [SerializeField] protected RunTime runTime;

    protected virtual void Update()
    {
        this.Washing();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRunTime();
    }

    protected override void ResetValue()
    {
        if (runTime == null) return;
        this.runTime.SetTimeMax(washTimeMax);
    }

    protected virtual void LoadRunTime()
    {
        if (runTime != null) return;
        this.runTime = GetComponent<RunTime>();
    }

    public override void Interact(PlayerCtrl playerCtrl)
    {
        PlayerHold playerHold = playerCtrl.PlayerHold;

        bool counterHasItem = this.HasItem();
        bool playerHasItem = playerHold.HasItem();

        if (!counterHasItem && playerHasItem)
        {
            this.PlaceDirtyPlate(playerHold);
            return;
        }

        if (counterHasItem)
        {
            PlateCtrl plate = this.GetItem().GetComponent<PlateCtrl>();
            if (plate == null) return;

            if (plate.PlateState == PlateState.Dirty)
            {
                this.StartWash();
                return;
            }

            if (plate.PlateState == PlateState.Empty && !playerHasItem)
            {
                this.TakeCleanPlate(playerHold);
            }
        }
    }

    protected virtual void PlaceDirtyPlate(PlayerHold playerHold)
    {
        KitchenObjCtrl item = playerHold.CurrentItem;
        if (item.KitchenType != KitchenType.Plate) return;

        PlateCtrl plate = item.GetComponent<PlateCtrl>();
        if (plate == null) return;

        if (plate.PlateState != PlateState.Dirty)
        {
            Debug.Log("Plate Is Not Dirty");
            return;
        }
        item = playerHold.RemoveItem();
        this.SetItem(item);
    }

    protected virtual void TakeCleanPlate(PlayerHold playerHold)
    {
        PlateCtrl plate = this.GetItem().GetComponent<PlateCtrl>();

        if (plate.PlateState == PlateState.Dirty) return;

        KitchenObjCtrl item = this.TakeItem();
        playerHold.HoldItem(item);
    }

    protected virtual void StartWash()
    {
        if (this.runTime.IsRunning) return;
        this.runTime.StartTime();
        InputManager.Instance.LockInput();

        Transform newBar = TimeBarSpawner.Instance.Spawn(TimeBarSpawner.TimeBar, this.holdPoint.position, this.holdPoint.rotation);
        TimeBar timeBar = newBar.GetComponent<TimeBar>();
        timeBar.SetRunTime(this.runTime);
    }

    protected virtual void Washing()
    {
        if (!this.runTime.IsRunning) return;
        if (!this.runTime.IsDone()) return;

        PlateCtrl plate = this.GetItem().GetComponent<PlateCtrl>();
        plate.SetEmpty();

        this.runTime.StopTime();
        InputManager.Instance.UnlockInput();
    }
}
