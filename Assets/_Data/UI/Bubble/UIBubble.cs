using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBubble : LoadMonoBehaviour
{
    [SerializeField] protected CustomerCtrl currentCustomer;
    public CustomerCtrl CurrentCustomer => currentCustomer;

    [SerializeField] protected UIItemSpawner uiItemSpawner;
    public UIItemSpawner UIItemSpawner => uiItemSpawner;

    [SerializeField] protected FollowTarget followTarget;
    public FollowTarget FollowTarget => followTarget;

    [SerializeField] protected ProgressBar progressBar;
    public ProgressBar ProgressBar => progressBar;

    [SerializeField] protected Transform content;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadUIItemSpawner();
        this.LoadFollowTarget();
        this.LoadProgressBar();
        this.LoadContent();
    }

    protected virtual void LoadUIItemSpawner()
    {
        if (this.uiItemSpawner != null) return;
        this.uiItemSpawner = GetComponentInChildren<UIItemSpawner>();
    }

    protected virtual void LoadFollowTarget()
    {
        if (this.followTarget != null) return;
        this.followTarget = GetComponent<FollowTarget>();
    }

    protected virtual void LoadProgressBar()
    {
        if (this.progressBar != null) return;
        this.progressBar = GetComponentInChildren<ProgressBar>();
    }

    protected virtual void LoadContent()
    {
        if (this.content != null) return;
        this.content = transform.Find("Content");
        this.content.gameObject.SetActive(false);
    }

    public virtual void SetCustomer(CustomerCtrl customer)
    {
        this.currentCustomer = customer;
        this.followTarget.SetTarget(customer.transform);
    }

    public void Init(List<DishSO> list)
    {
        this.content.gameObject.SetActive(true);
        this.uiItemSpawner.SpawnUIItem(list);
    }

    public void RemoveDish(DishSO dishSO)
    {
        this.uiItemSpawner.RemoveUIItem(dishSO);
    }

    public virtual void ResetBubble()
    {
        this.uiItemSpawner.ClearUIItem();
        this.progressBar.ResetBar();
        this.currentCustomer = null;
        this.followTarget.SetTarget(null);
        this.content.gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        this.UpdateProgressBar();
    }

    protected virtual void UpdateProgressBar()
    {
        if (this.currentCustomer == null)
        {
            this.progressBar.Hide();
            return;
        }

        CustomerOrder order = this.currentCustomer.CustomerOrder;

        if (!order.TimeRunning)
        {
            this.progressBar.Hide();
            return;
        }

        this.progressBar.Show();

        this.progressBar.SetProgress(order.WaitTimer, order.MaxTimer);
    }
}
