using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : LoadMonoBehaviour
{
    private static GameTime instance;
    public static GameTime Instance => instance;

    [SerializeField] protected float levelTime = 180f;
    public float LevelTime => levelTime;

    [SerializeField] protected float currentTime;
    public float CurrentTime => currentTime;

    protected override void Awake()
    {
        base.Awake();
        if (GameTime.instance != null) Debug.LogError("Only 1 GameTime allow to exist");
        GameTime.instance = this;
    }

    protected override void Start()
    {
        this.currentTime = levelTime;
        //UIResultPanel.Instance.Hide();
    }

    protected virtual void Update()
    {
        this.Timing();
    }

    protected virtual void Timing()
    {
        if (!GameManager.Instance.GameStart) return;

        if (this.currentTime > 0) this.currentTime -= Time.deltaTime;
        else this.currentTime = 0;
    }

    public virtual bool IsTimeUp()
    {
        return this.currentTime <= 0;
    }

    public virtual void ResetTime()
    {
        this.currentTime = this.levelTime;
    }
}
