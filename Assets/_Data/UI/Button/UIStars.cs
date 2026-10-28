using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIStars : StarManager
{
    [Header("UI Stars")]
    [SerializeField] protected float delayStars = 0.4f;

    public virtual int CalculateStars(int score)
    {
        LevelSO level = LevelManager.Instance.CurrentLevel;

        if (score >= level.threeStarScore) return 3;
        if (score >= level.twoStarScore) return 2;
        if (score >= level.oneStarScore) return 1;
        return 0;
    }

    public virtual void ResetUIStar()
    {
        for (int i = 0; i < this.starImages.Length; i++)
        {
            this.starImages[i].sprite = this.grayStar;
        }
    }

    public virtual IEnumerator ShowStarsRoutine(int totalStars)
    {
        //for (int i = 0; i < this.starImages.Length; i++)
        //{
        //    this.starImages[i].sprite = this.starEmptySprite;
        //}

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < totalStars; i++)
        {
            this.starImages[i].sprite = this.godStar;

            yield return new WaitForSecondsRealtime(this.delayStars);
        }
    }
}
