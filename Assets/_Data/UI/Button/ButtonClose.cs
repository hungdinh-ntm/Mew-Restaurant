using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClose : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        transform.parent.gameObject.SetActive(false);
    }
}
