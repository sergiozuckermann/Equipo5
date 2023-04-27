using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCInteractor : MonoBehaviour
{
    public GameObject canvas;
    public GameObject Shaggy;
    public GameObject Enemy;
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
    
    public void onstartbattlebutton(){
        unit Sh = Shaggy.GetComponent<unit>();
        string savedShaggy=JsonUtility.ToJson(Sh.stats);
        PlayerPrefs.SetString("Shaggy", savedShaggy);
        unit En = Enemy.GetComponent<unit>();
        PlayerPrefs.SetInt("Enemy", En.stats.index);
        SceneManager.LoadScene("Bosque_Combate");
    }
}
