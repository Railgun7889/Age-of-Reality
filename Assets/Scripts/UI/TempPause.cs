using UnityEngine;
using UnityEngine.SceneManagement;

public class TempPause : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject statsMenu;
    public GameObject itemMenu;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause_Game();
            }
        }
    }
    public void ChangeScene(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    public void Quit_Game()
    {
        Application.Quit();
    }

    public void Pause_Game()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        stats();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void stats()
    {
        statsMenu.SetActive(true);
        itemMenu.SetActive(false);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void items()
    {
        statsMenu.SetActive(false);
        itemMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
}
