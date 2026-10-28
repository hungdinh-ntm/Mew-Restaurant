using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTime : BaseText
{
    protected virtual void Update()
    {
        this.UpdateCountdown();
    }

    protected virtual void UpdateCountdown()
    {
        int currentTime = (int)GameTime.Instance.CurrentTime;
        int levelTime = (int)GameTime.Instance.LevelTime;
        int minutes = currentTime / 60;
        int secords = currentTime % 60;

        if (currentTime > levelTime / 3)
            this.textUI.color = Color.white;
        else this.textUI.color = Color.yellow;

        this.textUI.text = string.Format("{0:00}:{1:00}", minutes, secords);


    }
}
