using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePopup : LoadMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textUI;

    protected override void LoadComponent()
    {
        this.LoadText();
    }

    protected virtual void LoadText()
    {
        if (this.textUI != null) return;
        this.textUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public virtual void SetText(string text, Color color)
    {
        this.textUI.SetText(text);
        this.textUI.color = color;
    }

    public virtual void ResetPopup()
    {
        this.textUI.SetText("0");
    }
}
