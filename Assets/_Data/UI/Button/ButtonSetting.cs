using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSetting : BaseButton
{
    [Header("Button Setting")]
    [SerializeField] protected GameObject settingMenuUI;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSettingMenuUI();
    }

    protected virtual void LoadSettingMenuUI()
    {
        if (this.settingMenuUI != null) return;
        this.settingMenuUI = GameObject.Find("SettingMenu");

        this.settingMenuUI.SetActive(false);
    }

    protected override void OnClick()
    {
        base.OnClick();
        AudioManger.Instance.PlayButtonClick();
        this.settingMenuUI.SetActive(true);
    }
}
