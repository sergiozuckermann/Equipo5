//Code by Zaza Team
// Description: This script is used to Pause the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 
public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    //If ESQ is pressed, the game will be paused.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                PauseGame();
            }
        }
    }

    //This function is used to resume the game.
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    //This function is used to pause the game.
    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        Debug.Log("Loading Menu...");
    }
    public void QuitGame(){
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}