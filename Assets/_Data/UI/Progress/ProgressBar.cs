using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : LoadMonoBehaviour
{
    [Header("Progress Bar")]
    [SerializeField] protected ProgressSlider progressSlider;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadProgressSlider();
    }

    protected virtual void LoadProgressSlider()
    {
        if (this.progressSlider != null) return;
        this.progressSlider = GetComponentInChildren<ProgressSlider>();
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void SetProgress(float current, float max)
    {
        this.progressSlider.SetValue(current, max);
    }

    public virtual void ResetBar()
    {
        this.SetProgress(0, 1);
        this.Hide();
    }
}
