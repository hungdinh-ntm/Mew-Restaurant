using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolSO", menuName = "SO /ToolSO")]
public class ContainerSO : ScriptableObject
{
    public List<FoodSO> allowedFoods;
}
