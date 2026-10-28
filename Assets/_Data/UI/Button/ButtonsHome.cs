using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsHome : LoadMonoBehaviour
{
    public virtual void GoMain()
    {
        AudioManger.Instance.PlayButtonClick();
        SceneManager.LoadScene("MainScene");
    }

    public virtual void NewGame()
    {
        Debug.Log("Reload Data");

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        this.GoMain();
    }
}
