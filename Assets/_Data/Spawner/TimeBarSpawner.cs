using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBarSpawner : Spawner
{
    private static TimeBarSpawner instance;
    public static TimeBarSpawner Instance => instance;

    public static string TimeBar = "TimeBar";

    protected override void Awake()
    {
        base.Awake();
        if (TimeBarSpawner.instance != null) Debug.LogError("Only 1 TimeBarSpawner allow to exist");
        TimeBarSpawner.instance = this;
    }
}

