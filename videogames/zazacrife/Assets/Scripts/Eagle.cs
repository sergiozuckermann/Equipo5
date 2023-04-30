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
            PlayerPrefs.SetInt("place", 1);
            SceneManager.LoadScene("Torre");
        }

        else
        {
            dialogueText.text = "I SAID GET THE THREE ELEMENTS YOU DINGUS!";
        }
        
    }
}
