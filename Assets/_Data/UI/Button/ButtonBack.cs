using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBack : BaseButton
{
    protected override void OnClick()
    {
        base.OnClick();
        SceneManager.LoadScene("HomeScene");
    }
}
