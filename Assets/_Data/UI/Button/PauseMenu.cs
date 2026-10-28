using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : LoadMonoBehaviour
{
    [SerializeField] protected GameObject pauseMenu;

    public void Pause()
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameOver) return;
        AudioManger.Instance.PlayButtonClick();

        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Home()
    {
        AudioManger.Instance.PlayButtonClick();
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }
    public void Resume()
    {
        AudioManger.Instance.PlayButtonClick();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        AudioManger.Instance.PlayButtonClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
