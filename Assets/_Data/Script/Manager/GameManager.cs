using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : LoadMonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] protected bool gameStart = false;
    public bool GameStart => gameStart;

    [SerializeField] protected bool gameOver = false;
    public bool GameOver => gameOver;

    [SerializeField] protected bool customerCanSpawn = false;
    public bool CustomerCanSpawn => customerCanSpawn;

    protected override void Awake()
    {
        base.Awake();
        if (GameManager.instance != null) Debug.LogError("Only 1 GameManager allow to exist");
        GameManager.instance = this;
    }

    protected override void Start()
    {
        base.Start();
        UIResultPanel.Instance.Hide();
        Time.timeScale = 1f;
    }

    protected virtual void Update()
    {
        this.CheckGameOver();
    }

    protected virtual void CheckGameOver()
    {
        if (this.gameOver) return;

        if (!GameTime.Instance.IsTimeUp()) return;

        this.EndLevel();
    }

    protected virtual void EndLevel()
    {
        int score = ScoreManager.Instance.ScoreGame;

        this.gameOver = true;
        PlayerCtrl.Instance.Rb.velocity = Vector2.zero;
        //Time.timeScale = 0f;
        UIResultPanel.Instance.ShowPanelScore(score);
        

        Debug.Log("LEVEL COMPLETE");
    }

    public virtual void StartGame()
    {
        this.gameStart = true;
        AudioManger.Instance.PlayBell();
        StartCoroutine(EnableCustomerSpawn());
    }

    protected virtual IEnumerator EnableCustomerSpawn()
    {
        yield return new WaitForSeconds(8f);

        this.customerCanSpawn = true;
    }
}
