using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : LoadMonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance => instance;

    [SerializeField] protected LevelSO currentLevel;
    public LevelSO CurrentLevel => currentLevel;

    [SerializeField] protected List<LevelSO> listLevels;
    public List<LevelSO> ListLevels => listLevels;

    protected override void Awake()
    {
        base.Awake();

        if (LevelManager.instance != null && LevelManager.instance != this)
        {
            Destroy(gameObject);
            return;
        }

        LevelManager.instance = this;
        DontDestroyOnLoad(gameObject);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadListLevels();
    }

    protected virtual void LoadListLevels()
    {
        if (this.listLevels.Count > 0) return;
        this.listLevels = Resources.LoadAll<LevelSO>("SO/Level").ToList();
    }

    public virtual void SetLevel(LevelSO levelSO)
    {
        this.currentLevel = levelSO;
    }

    public virtual bool IsUnlocked(LevelSO levelSO)
    {
        int index = this.listLevels.IndexOf(levelSO);
        if (index == 0) return true;

        LevelSO previousLevel = this.listLevels[index - 1];
        return SaveManager.LoadStar(previousLevel.sceneName) >= 1;
    }
}
