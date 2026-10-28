using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LevelSO", menuName = "SO /LevelSO")]
public class LevelSO : ScriptableObject
{
    public string sceneName;
    public string levelName;
    public Sprite levelPreview;

    [Header("Level Status")]
    public bool isDesigned;

    [Header("Star")]
    public int oneStarScore;
    public int twoStarScore;
    public int threeStarScore;

    [Header("Menu")]
    public List<DishSO> mainDishMenu;
    public List<DishSO> dessertMenu;
}
