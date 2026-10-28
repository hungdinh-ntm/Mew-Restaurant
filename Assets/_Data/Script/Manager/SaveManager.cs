using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : LoadMonoBehaviour
{
    public static void SaveStar(string levelName, int star)
    {
        string key = levelName + "_Star";
        int oldStar = LoadStar(levelName);

        if (star > oldStar)
        {
            PlayerPrefs.SetInt(key, star);
            PlayerPrefs.Save();
        }
    }

    public static int LoadStar(string levelName)
    {
        string key = levelName + "_Star";
        return PlayerPrefs.GetInt(key, 0);
    }

    public static void SaveBestScore(string levelName, int score)
    {
        string key = levelName + "_BestScore";
        int oldScore = LoadBestScore(levelName);

        if (score > oldScore)
        {
            PlayerPrefs.SetInt(key, score);
            PlayerPrefs.Save();
        }
    }

    public static int LoadBestScore(string levelName)
    {
        string key = levelName + "_BestScore";
        return PlayerPrefs.GetInt(key, 0);
    }

    public static void DeleteLevelData(string levelName)
    {
        PlayerPrefs.DeleteKey(levelName + "_Star");
        PlayerPrefs.DeleteKey(levelName + "_BestScore");
        PlayerPrefs.Save();
    }

    //public static void DeleteAllData()
    //{
    //    PlayerPrefs.DeleteAll();
    //    PlayerPrefs.Save();
    //}
}
