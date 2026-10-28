
using UnityEngine;

public class ButtonLevelSpawner : Spawner
{
    private static ButtonLevelSpawner instance;
    public static ButtonLevelSpawner Instance => instance;

    public static string button = "Button";

    [SerializeField] protected UIButtonCtrl uiButtonCtrl;

    protected override void Awake()
    {
        base.Awake();
        if (ButtonLevelSpawner.instance != null) Debug.LogError("Only 1 ButtonLevelSpawner allow to exist");
        ButtonLevelSpawner.instance = this;
    }

    protected virtual void LoadUIButtonCtrl()
    {
        if (this.uiButtonCtrl != null) return;
        this.uiButtonCtrl = transform.parent.GetComponent<UIButtonCtrl>();
    }

    protected override void LoadHolder()
    {
        this.LoadUIButtonCtrl();

        if (this.holder != null) return;
        this.holder = this.uiButtonCtrl.Panel;
    }

    public virtual void SpawnButton(LevelSO levelSO)
    {
        Transform newButton = this.Spawn(ButtonLevelSpawner.button, Vector3.zero, Quaternion.identity);
        newButton.transform.localScale = new Vector3(1, 1, 1);

        ButtonLevel buttonLevel = newButton.GetComponent<ButtonLevel>();
        ButtonLevelManager.Instance.AddButton(buttonLevel);
        buttonLevel.SetLevel(levelSO);

        buttonLevel.name = "Button" + levelSO.sceneName;

        
    }
}

