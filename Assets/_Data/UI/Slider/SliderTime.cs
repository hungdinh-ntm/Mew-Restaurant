using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTime : BaseSlider
{
    [Header("Slider Time")]
    [SerializeField] protected float maxTimer;
    [SerializeField] protected float currentTimer;

    protected virtual void FixedUpdate()
    {
        this.TimeShowing();
    }

    protected virtual void TimeShowing()
    {
        if (this.maxTimer <= 0)
        {
            this.slider.value = 0;
            return;
        }

        float TimerPrecent = this.currentTimer / this.maxTimer;
        this.slider.value = TimerPrecent;
        //Debug.Log("timer precent: " + TimerPrecent);
    }

    public virtual void SetValue(float current, float max)
    {
        this.currentTimer = current;
        this.maxTimer = max;
    }
}
