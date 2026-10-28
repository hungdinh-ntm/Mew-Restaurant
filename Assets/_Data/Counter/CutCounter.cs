
using UnityEngine;

public class CutCounter : BaseCounter
{
    [Header("Cut Counter")]
    [SerializeField] protected CounterState state = CounterState.Empty;
    [SerializeField] protected float cutTimeMax = 1f;

    [SerializeField] protected RunTime runTime;

    private void Update()
    {
        this.Processing();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRunTime();
    }

    protected override void ResetValue()
    {
        if (runTime == null) return;
        this.runTime.SetTimeMax(cutTimeMax);
    }

    protected virtual void LoadRunTime()
    {
        if (runTime != null) return;
        this.runTime = GetComponent<RunTime>();
    }

    public override void Interact(PlayerCtrl playerCtrl)
    {
        PlayerHold playerHold = playerCtrl.PlayerHold;

        if (this.HasItem() && playerHold.HasItem())
        {
            FoodCtrl food = playerHold.CurrentItem.GetComponent<FoodCtrl>();
            if (!this.CheckFood(food))
            {
                return;
            }

            this.SwapItem(playerHold);
            this.state = CounterState.HasItem;
            return;
        }

        switch (state)
        {
            case CounterState.Empty:
                this.TryPlaceItem(playerHold);
                break;

            case CounterState.HasItem:
                this.TryStartCut();
                break;

            case CounterState.Complete:
                this.TryTakeItem(playerHold);
                break;
        }
    }

    protected virtual void TryPlaceItem(PlayerHold playerHold)
    {
        if (!playerHold.HasItem()) return;

        KitchenObjCtrl item = playerHold.CurrentItem;
        if (item.KitchenType != KitchenType.Food)
        {
            Debug.Log("not food type");
            return;
        }

        FoodCtrl food = item.GetComponent<FoodCtrl>();

        if (!this.CheckFood(food)) return;

        item = playerHold.RemoveItem();
        this.SetItem(item);
        state = CounterState.HasItem;
    }

    protected virtual void TryStartCut()
    {
        if (this.runTime.IsRunning) return;

        this.runTime.StartTime();
        InputManager.Instance.LockInput();
        this.state = CounterState.Processing;

        Transform newBar = TimeBarSpawner.Instance.Spawn(TimeBarSpawner.TimeBar, this.holdPoint.position, this.holdPoint.rotation);
        TimeBar timeBar = newBar.GetComponent<TimeBar>();
        timeBar.SetRunTime(this.runTime);
    }

    protected virtual void Processing()
    {
        if (state != CounterState.Processing) return;
        if (!this.runTime.IsDone()) return; ;

        FoodCut foodCut = this.itemCounter.GetComponentInChildren<FoodCut>();

        if (foodCut != null) foodCut.Cut();
        this.runTime.StopTime();

        state = CounterState.Complete;
        InputManager.Instance.UnlockInput();
    }

    protected virtual void TryTakeItem(PlayerHold playerHold)
    {
        KitchenObjCtrl item = this.TakeItem();

        playerHold.HoldItem(item);
        state = CounterState.Empty;
    }

    protected virtual bool CheckFood(FoodCtrl food)
    {
        bool foodCanCut = food.FoodAbility.CanCut;
        bool foodSliced = food.FoodCut?.CheckCanCut() ?? false;

        if (!(foodCanCut && foodSliced))
        {
            Debug.Log(food.name + " can't cut");
            return false;
        }

        return true;
    }
}
