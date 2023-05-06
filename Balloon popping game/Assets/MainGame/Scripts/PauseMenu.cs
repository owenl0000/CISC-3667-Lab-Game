using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject directionsUI;
    public GameObject sceneButtons;
    public PlayerMovement player;
    // Update is called once per frame

    void Start() {
        pauseMenuUI.SetActive(false);
        directionsUI.SetActive(false);
        player = FindObjectOfType<PlayerMovement>();

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else 
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        sceneButtons.SetActive(true);
        player.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {   
        sceneButtons.SetActive(false);
        player.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Menu() 
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameManager.Instance.HideCanvas();
        PersistentData.Instance.SetScore(0);
        PersistentData.Instance.SetName("");
        SceneManager.LoadScene("Menu");
    }

    public void directionsOn()
    {
        player.enabled = false;
        sceneButtons.SetActive(false);
        directionsUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void directionsOff()
    {
        player.enabled = true;
        sceneButtons.SetActive(true);
        directionsUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
