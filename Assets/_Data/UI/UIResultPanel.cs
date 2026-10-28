using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIResultPanel : LoadMonoBehaviour
{
    private static UIResultPanel instance;
    public static UIResultPanel Instance => instance;

    [SerializeField] protected UIStars uistars;

    protected override void Awake()
    {
        base.Awake();
        if (UIResultPanel.instance != null) Debug.LogError("Only 1 ResultPanel allow to exist");
        UIResultPanel.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadUIStars();
    }

    protected virtual void LoadUIStars()
    {
        if (this.uistars != null) return;
        this.uistars = transform.GetComponentInChildren<UIStars>();
    }

    public virtual void ShowPanelScore(int finalScore)
    {
        this.uistars.ResetUIStar();
        transform.gameObject.SetActive(true);

        int starsEarned = this.uistars.CalculateStars(finalScore);

        LevelSO level = LevelManager.Instance.CurrentLevel;
        SaveManager.SaveStar(level.sceneName, starsEarned);
        SaveManager.SaveBestScore(level.sceneName, finalScore);

        StartCoroutine(this.uistars.ShowStarsRoutine(starsEarned));
    }

    public virtual void Hide()
    {
        transform.gameObject.SetActive(false);
    }
}
