using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Spawner
{
    private static ItemSpawner instance;
    public static ItemSpawner Instance => instance;

    public static string Onion = "Onion";
    public static string Tomato = "Tomato";
    public static string Lettuce = "Lettuce";
    public static string OrangeJuice = "OrangeJuice";
    public static string IceCream = "IceCream";

    protected override void Awake()
    {
        base.Awake();
        if (ItemSpawner.instance != null) Debug.LogError("Only 1 ItemSpawner allow to exist");
        ItemSpawner.instance = this;
    }
}
