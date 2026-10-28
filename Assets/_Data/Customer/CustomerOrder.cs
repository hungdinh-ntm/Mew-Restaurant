using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CustomerOrder : CustomerAbstract
{
    [Header("Customer Order")]
    [SerializeField] protected List<DishSO> dishOrders = new();
    public List<DishSO> DishOrders => dishOrders;

    [SerializeField] protected float waitTimer = 15f;
    public float WaitTimer => waitTimer;

    [SerializeField] protected float maxTimer = 30f;
    public float MaxTimer => maxTimer;

    [SerializeField] protected bool timeRunning = false;
    public bool TimeRunning => timeRunning;

    [SerializeField] protected int penalty = 5;

    protected virtual void Update()
    {
        this.WaitTime();
    }

    protected virtual void StartWaitTime()
    {
        this.waitTimer = this.maxTimer;
        this.timeRunning = true;
    }

    protected virtual void StopWaitTime()
    {
        this.waitTimer = this.maxTimer;
        this.timeRunning = false;
    }

    protected virtual void WaitTime()
    {
        if (GameManager.Instance.GameOver) return;

        if (!this.timeRunning) return;
        this.waitTimer -= Time.deltaTime;
        if (this.waitTimer > 0) return;

        this.TimeOut();
    }

    protected virtual void TimeOut()
    {
        this.PenaltyScore();

        this.StopWaitTime();
        this.customerCtrl.LeaveRestaurant();
    }

    public virtual void PauseTimer()
    {
        this.timeRunning = false;
    }

    public virtual void ResumeTimer()
    {
        this.timeRunning = true;
    }

    protected virtual void PenaltyScore()
    {
        int score = 0;

        if (this.customerCtrl.CustomerState == CustomerState.WaitOrder)
        {
            score = this.penalty;

        }
        else if (this.customerCtrl.CustomerState == CustomerState.WaitDish)
        {
            score = this.penalty * dishOrders.Count;

        }
        ScoreManager.Instance.MinusScore(score);
        this.AddPopupToObj("-" + score, Color.red);
    }

    public virtual void CallStaff()
    {
        if (this.customerCtrl.CustomerState != CustomerState.WaitOrder) return;

        AudioManger.Instance.PlayMeow();
        this.ClearOrder();
        List<DishSO> orders = OrderManager.Instance.Order();
        this.dishOrders.AddRange(orders);

        string allDishes = string.Join(", ", this.dishOrders);
        //Debug.Log("Customer need: " + allDishes);
        this.StartWaitTime();
        this.AddBubbleToObj();


    }

    public virtual void WaitDish()
    {
        this.customerCtrl.SetState(CustomerState.WaitDish);
        this.customerCtrl.UIBubble.Init(this.dishOrders);

        this.StartWaitTime();
    }

    public virtual bool DeliveryOrder(DishSO dishSO)
    {
        if (!this.dishOrders.Contains(dishSO))
        {
            Debug.Log("Wrong Dish! Try Again.");
            return false;
        }
        Debug.Log("Correct Dish:" + dishSO.name);
        return true;
    }

    public virtual void ClearOrder()
    {
        this.StopWaitTime();
        this.dishOrders.Clear();
    }

    public virtual void RemoveDish(DishSO dishSO)
    {
        if (!this.dishOrders.Contains(dishSO)) return;

        ScoreManager.Instance.AddScore(dishSO.score);
        this.AddPopupToObj("+" + dishSO.score, Color.yellow);

        this.dishOrders.Remove(dishSO);
        this.customerCtrl.UIBubble.RemoveDish(dishSO);

        if (this.HasFinishedEating())
        {
            this.StopWaitTime();
            this.customerCtrl.LeaveRestaurant();
            Debug.Log("Customer Leave");
        }
    }

    public virtual bool HasFinishedEating()
    {
        return this.dishOrders.Count <= 0;
    }

    protected virtual void AddBubbleToObj()
    {
        Transform newBubble = UIBubbleSpawner.Instance.Spawn(UIBubbleSpawner.UIBubble, transform.parent.position, Quaternion.identity);

        UIBubble uiBubble = newBubble.GetComponent<UIBubble>();
        this.customerCtrl.SetBubble(uiBubble);
    }

    protected virtual void AddPopupToObj(string score, Color color)
    {
        Transform obj = ScorePopupSpawner.Instance.SpawnPopup(transform.parent.position, Quaternion.identity);

        ScorePopup popup = obj.GetComponent<ScorePopup>();
        popup.SetText(score, color);
    }
}
