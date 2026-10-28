using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressSlider : BaseSlider
{
    [Header("Progress Slider")]
    [SerializeField] protected Image fillImage;

    [SerializeField] protected float currentValue;
    [SerializeField] protected float maxValue = 1;

    protected virtual void Update()
    {
        this.UpdateSlider();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadFillImage();
    }

    protected virtual void LoadFillImage()
    {
        if (this.fillImage != null) return;
        this.fillImage = transform.Find("Fill Area/Fill").GetComponent<Image>();
    }

    protected virtual void UpdateSlider()
    {
        if (GameManager.Instance.GameOver) return;
        if (maxValue <= 0) return;

        float percent = currentValue / maxValue;
        this.slider.value = percent;

        if (percent > 0.5f)
            fillImage.color = Color.green;
        else if (percent > 0.25f)
            fillImage.color = Color.yellow;
        else
            fillImage.color = Color.red;
    }

    public virtual void SetValue(float current, float max)
    {
        this.currentValue = current;
        this.maxValue = max;
    }
}
