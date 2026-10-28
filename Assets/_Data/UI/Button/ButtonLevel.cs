using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : BaseButton
{
    [SerializeField] protected LevelSO levelSO;
    [SerializeField] protected TextMeshProUGUI textLevelName;
    [SerializeField] protected StarManager starManager;
    [SerializeField] protected Image buttonImage;
    [SerializeField] protected Image tickButton;
    [SerializeField] protected Image lockImage;

    public virtual void SetLevel(LevelSO levelSO)
    {
        this.levelSO = levelSO;

        this.LoadTextLevelName();
        this.LoadStarManager();
        this.LoadButtonImage();
        this.LoadTickButton();

        this.LoadUI();
    }

    protected virtual void LoadTextLevelName()
    {
        if (this.textLevelName != null) return;
        this.textLevelName = transform.Find("LevelName").GetComponent<TextMeshProUGUI>();

        this.textLevelName.SetText(this.levelSO.levelName);
    }

    protected virtual void LoadStarManager()
    {
        if (this.starManager != null) return;
        this.starManager = transform.Find("Stars").GetComponent<StarManager>();
    }

    protected virtual void LoadButtonImage()
    {
        if (this.buttonImage != null) return;
        this.buttonImage = transform.Find("Image").GetComponent<Image>();
    }

    protected virtual void LoadTickButton()
    {
        if (this.tickButton != null) return;
        this.tickButton = transform.Find("Tick").GetComponent<Image>();

        this.HideTickButton();
    }

    protected virtual void LoadUI()
    {
        this.textLevelName.SetText(this.levelSO.levelName);

        int starCount = SaveManager.LoadStar(this.levelSO.sceneName);
        this.starManager.LoadMainUIStar(starCount);
        this.buttonImage.sprite = this.levelSO.levelPreview;

        bool unlock = LevelManager.Instance.IsUnlocked(levelSO);
        this.lockImage.gameObject.SetActive(!unlock);
        GetComponent<Button>().interactable = unlock;
    }

    protected override void OnClick()
    {
        base.OnClick();

        LevelManager.Instance.SetLevel(levelSO);
        Debug.Log("Selected: " + levelSO.sceneName);

        ButtonLevelManager.Instance.HideAllTickButton();

        this.tickButton.gameObject.SetActive(true);

        Debug.Log(this.levelSO.sceneName + " Score: " + SaveManager.LoadBestScore(this.levelSO.sceneName));
        Debug.Log(this.levelSO.sceneName + " Star: " + SaveManager.LoadStar(this.levelSO.sceneName));
    }

    public virtual void HideTickButton()
    {
        this.tickButton.gameObject.SetActive(false);
    }
}
