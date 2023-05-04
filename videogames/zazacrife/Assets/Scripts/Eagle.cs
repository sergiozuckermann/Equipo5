//Made by Zaza Team
// Description: This script is used the same as NPC interactor (to manage NPC interactions with SemiBosses) but with the Eagle to send the player to the tower if the three elements are active.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class Eagle : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject canvas;
    public GameObject Shaggy;
    public Button button;

    private void Start()
    {
        canvas.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.SetActive(true);
            
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.SetActive(false);
        }
    }
    
    public void onstarteaglebutton(){
        unit Sh = Shaggy.GetComponent<unit>();

        if (Sh.stats.firea == true && Sh.stats.icea == true && Sh.stats.lightninga == true){
            string savedShaggy=JsonUtility.ToJson(Sh.stats);
            PlayerPrefs.SetString("Shaggy", savedShaggy);
            PlayerPrefs.SetFloat("x", Convert.ToSingle(-3.9));
            PlayerPrefs.SetFloat("y", Convert.ToSingle(3.5));
            Sh.stats.place = 2;
            PlayerPrefs.SetInt("place", 2);
            SceneManager.LoadScene("Torre");
        }

        
    }
}
