using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStart : BaseButton
{
    [SerializeField] protected GameObject notificationPanel;

    //protected override void LoadComponent()
    //{
    //    base.LoadComponent();
    //    this.LoadNotificationPanel();
    //}

    //protected virtual void LoadNotificationPanel()
    //{
    //    if (notificationPanel != null) return;
    //    this.notificationPanel = Find("NotificationPanel").gameObject;
    //    this.notificationPanel.SetActive(false);
    //}

    protected override void OnClick()
    {
        base.OnClick();

        LevelSO level = LevelManager.Instance.CurrentLevel;

        if (level == null)
        {
            Debug.Log("Don't have level");
            return;
        }

        if (!level.isDesigned)
        {
            Debug.Log("Level is currently being designed");

            this.notificationPanel.SetActive(true);
            return;
        }

        SceneManager.LoadScene(level.sceneName);
    }
}
