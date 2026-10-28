using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseText : LoadMonoBehaviour
{
    [Header("Base Text")]
    [SerializeField] protected TextMeshProUGUI textUI;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadText();
    }

    protected virtual void LoadText()
    {
        if (this.textUI != null) return;
        this.textUI = GetComponent<TextMeshProUGUI>();
    }
}
