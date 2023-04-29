using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class FinalBoss : MonoBehaviour
{
    public GameObject Enemy;


    public void onstartbattlebutton(){
        unit En = Enemy.GetComponent<unit>();
        PlayerPrefs.SetInt("Enemy", En.stats.index);
        PlayerPrefs.SetInt("Number", En.stats.number);
        SceneManager.LoadScene("Bosque_Combate");
    }
}
