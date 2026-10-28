using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScoreGame : BaseText
{
    protected virtual void Update()
    {
        this.UpdateScore();
    }

    protected virtual void UpdateScore()
    {
        int score = ScoreManager.Instance.ScoreGame;

        this.textUI.SetText(score.ToString());
    }
}
