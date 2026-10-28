using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DishSO", menuName = "SO /DishSO")]
public class DishSO : ScriptableObject
{
    public string dishName;
    public Sprite sprite;

    public int score;
}
