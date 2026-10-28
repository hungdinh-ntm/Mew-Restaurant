using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBar : LoadMonoBehaviour
{
    [Header("Time Bar")]
    [SerializeField] protected RunTime runTime;
    [SerializeField] protected SliderTime sliderTime;
    [SerializeField] protected Spawner spawner;

    protected virtual void Update()
    {
        this.UpdateTimeBar();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSliderTime();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = transform.parent.parent.GetComponent<Spawner>();
    }

    protected virtual void LoadSliderTime()
    {
        if (this.sliderTime != null) return;
        this.sliderTime = GetComponentInChildren<SliderTime>();
    }

    protected virtual void UpdateTimeBar()
    {
        if (this.runTime == null) return;
        bool isDone = this.runTime.IsDone();
        if (isDone)
        {
            //this.runTime.ResetTime();
            this.spawner.Despawn(transform);
            return;
        }

        float timeMax = this.runTime.TimeMax;
        float currentTime = this.runTime.CurrentTime;
        //Debug.Log("timemax / currenttime:" + timeMax + " / " + currentTime);
        this.sliderTime.SetValue(currentTime, timeMax);
    }

    public virtual void ResetTimeBar()
    {
        this.sliderTime.SetValue(1, 1);
    }

    public virtual void SetRunTime(RunTime runTime)
    {
        this.runTime = runTime;
    }
}
