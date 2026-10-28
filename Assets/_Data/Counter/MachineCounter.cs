using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineCounter : FoodCounter
{
    [SerializeField] protected PlayerCtrl currentPlayer;
    [SerializeField] protected RunTime runTime;
    [SerializeField] protected float makeTimeMax = 1f;
    [SerializeField] protected bool isMaking = false;

    private void Update()
    {
        MakeItem();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRunTime();
    }

    protected override void ResetValue()
    {
        if (runTime == null) return;
        this.runTime.SetTimeMax(makeTimeMax);
    }

    protected virtual void LoadRunTime()
    {
        if (runTime != null) return;
        this.runTime = GetComponent<RunTime>();
    }

    public override void Interact(PlayerCtrl playerCtrl)
    {
        if (this.isMaking) return;
        if (this.runTime.IsRunning) return;

        PlayerHold playerHold = playerCtrl.PlayerHold;
        if (playerHold.HasItem()) return;

        this.currentPlayer = playerCtrl;
        this.isMaking = true;

        this.runTime.StartTime();
        InputManager.Instance.LockInput();

        Transform newBar = TimeBarSpawner.Instance.Spawn(TimeBarSpawner.TimeBar, transform.position, transform.rotation);
        TimeBar timeBar = newBar.GetComponent<TimeBar>();
        timeBar.SetRunTime(this.runTime);
    }

    protected virtual void MakeItem()
    {
        if (!this.isMaking) return;
        if (!this.runTime.IsDone()) return;

        this.runTime.StopTime();
        InputManager.Instance.UnlockInput();
        //this.runTime.ResetTime();

        PlayerHold playerHold = this.currentPlayer.PlayerHold;

        Vector2 pos = playerHold.HoldPoint.position;
        Quaternion ros = playerHold.HoldPoint.rotation;

        Transform newObj = ItemSpawner.Instance.Spawn(this.itemName, pos, ros);

        KitchenObjCtrl item = newObj.GetComponent<KitchenObjCtrl>();
        playerHold.HoldItem(item);

        this.currentPlayer = null;
        this.isMaking = false;
    }
}
