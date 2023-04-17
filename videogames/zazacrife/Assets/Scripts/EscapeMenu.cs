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

    // Update is called once per frame
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
    public void RestartLevel(){
        SceneManager.LoadScene(2);
    }
    public void Escape(){
        SceneManager.LoadScene(1);
    }
}
