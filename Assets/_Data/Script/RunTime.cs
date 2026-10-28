using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTime : LoadMonoBehaviour
{
    [SerializeField] protected float timeMax = 3f;
    public float TimeMax => timeMax;

    [SerializeField] protected float currentTime = 0f;
    public float CurrentTime => currentTime;

    [SerializeField] protected bool isRunning = false;
    public bool IsRunning => isRunning;

    protected virtual void Update()
    {
        if (GameManager.Instance.GameOver) return;
        if (!this.isRunning) return;
        this.currentTime += Time.deltaTime;
    }

    public virtual void StartTime()
    {
        this.currentTime = 0f;
        this.isRunning = true;
    }

    public virtual void StopTime()
    {
        this.isRunning = false;
    }

    public virtual bool IsDone()
    {
        return this.currentTime >= this.timeMax;
    }

    public virtual void SetTimeMax(float timeMax)
    {
        this.timeMax = timeMax;
    }
}
