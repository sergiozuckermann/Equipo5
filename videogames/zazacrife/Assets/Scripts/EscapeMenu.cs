//Code by Zaza Team
// Description: This script is used to Pause the game in combat.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EscapeMenu : MonoBehaviour
{
   public Button escape;
   public Button restart;
   public bool EscapeMenuOpen;

    // Update will check if the Escape key is pressed. If it is, the Escape Menu will be opened. If it is pressed again, the Escape Menu will be closed.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (EscapeMenuOpen == false){
                EscapeMenuOpen = true;
            
                escape.gameObject.SetActive(true);
                restart.gameObject.SetActive(true);
            }
            else {
                EscapeMenuOpen = false;
                escape.gameObject.SetActive(false);
                restart.gameObject.SetActive(false);
            }
        }
    }

    //This function is used to restart the level.
    public void RestartLevel(){
        SceneManager.LoadScene(2);
    }

    //This function is used to flee battle.
    public void Escape(){
        SceneManager.LoadScene(1);
    }
}
