using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : Spawner
{
    [Header("Customer Spawner")]
    private static CustomerSpawner instance;
    public static CustomerSpawner Instance => instance;

    //public static string Customer1 = "Customer1";
    //public static string Customer2 = "Customer2";
    //public static string Customer3 = "Customer3";

    protected override void Awake()
    {
        base.Awake();
        if (CustomerSpawner.instance != null) Debug.LogError("Only 1 CustomerSpawner allow to exist");
        CustomerSpawner.instance = this;
    }
}
