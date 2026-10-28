using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarManager : LoadMonoBehaviour
{
    [Header("Star Manager")]
    [SerializeField] protected Image[] starImages;
    [SerializeField] protected Sprite grayStar;
    [SerializeField] protected Sprite godStar;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadStars();
    }

    protected virtual void LoadStars()
    {
        if (this.starImages.Length > 0) return;
        this.starImages = transform.GetComponentsInChildren<Image>();
    }

    public virtual void LoadMainUIStar(int starCount)
    {
        //int starCount =
        //    SaveManager.LoadStar(this.levelSO.sceneName);

        for (int i = 0; i < this.starImages.Length; i++)
        {
            if (i < starCount)
                this.starImages[i].sprite = this.godStar;
            else
                this.starImages[i].sprite = this.grayStar;
        }
    }
}
