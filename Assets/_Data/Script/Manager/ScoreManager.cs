using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : LoadMonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance => instance;

    [SerializeField] protected int scoreGame = 0;
    public int ScoreGame => scoreGame;

    protected override void Awake()
    {
        base.Awake();
        if (ScoreManager.instance != null) Debug.LogError("Only 1 ScoreManager allow to exist");
        ScoreManager.instance = this;
    }

    public virtual void AddScore(int score)
    {
        this.scoreGame += score;

        AudioManger.Instance.PlayCoinCollect();
    }

    public virtual void MinusScore(int score)
    {
        this.scoreGame -= score;

        AudioManger.Instance.PlayCoinDrop();
    }

    public virtual void ResetScore()
    {
        this.scoreGame = 0;
    }
}
