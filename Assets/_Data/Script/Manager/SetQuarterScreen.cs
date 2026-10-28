using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetQuarterScreen : LoadMonoBehaviour
{
    protected override void Start()
    {
        base.Start();
        int screenWidth = Screen.currentResolution.width;
        int screenHeight = Screen.currentResolution.height;

        int targetWidth = screenWidth / 2;
        int targetHeight = screenHeight / 2;

        Screen.SetResolution(targetWidth, targetHeight, FullScreenMode.Windowed);
    }
}
