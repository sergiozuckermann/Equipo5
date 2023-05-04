// Made by Zaza Team
// Description: This script is used to manage the main menu and send the player the TileMap.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //This function is used to send the player to the TileMap.
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //This function is used to quit the game.
    public void QuitGame(){
        SceneManager.LoadScene(0);
    }
}
