//Made by Zaza Team
// Description: This script is used to manage NPC interactions with SemiBosses.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
 
public class NPCInteractor : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI bText;
    public GameObject canvas;
    public GameObject Shaggy;
    public GameObject Enemy;
    public Button button;
    public Animator anim;
    private unit EnemyUnit;
    public unit playerUnit;

    // Start will initialize the canvas and the enemy unit.
    private void Start()
    {
        canvas.SetActive(false);
        EnemyUnit=Enemy.GetComponent<unit>();
    }

    // Update will check if the enemy is dead to change the animation and destroy the button.
    void Update(){
        

        int enemyID=PlayerPrefs.GetInt("Dead");
        int Number=PlayerPrefs.GetInt("Number");

  
         if (enemyID == 1 && EnemyUnit.stats.number == Number)
         {
                anim.SetInteger("State", 4);
                
                Destroy(button);
                if (EnemyUnit.stats.number == 5 )
                {
                    dialogueText.text = "I AIN'T GOT THAT RIZZ NOMORE!";
                    bText.text = "YOU GOT FIRE!";
                }

                else if (EnemyUnit.stats.number == 6)
                {
                    dialogueText.text = "YOU CAUGHT ME I'M BARACK OBAMA";
                    bText.text = "YOU GOT THUNDER!";
                }

                else if (EnemyUnit.stats.number == 7)
                {
                    dialogueText.text = "IM AS DEAD AS KANYES CAREER";
                    bText.text = "YOU GOT ICE!";
                }

                else if (EnemyUnit.stats.number == 8)
                {
                    unit Sh = Shaggy.GetComponent<unit>();
                    string savedShaggy=JsonUtility.ToJson(Sh.stats);
                    PlayerPrefs.SetString("Shaggy", savedShaggy);
                    SceneManager.LoadScene("FinalBoss");
                }





         }
    }

   // OnTriggerEnter2D will check if the player is colliding with the NPC and will activate the canvas.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.SetActive(true);
            
        }

            if(playerUnit.stats.firea==true){
            if(EnemyUnit.stats.firea==true){
            Destroy(button);
            anim.SetInteger("State", 4);
            dialogueText.text = "I AIN'T GOT THAT RIZZ NOMORE!";
            bText.text = "YOU GOT FIRE!";
            }
        }

        if(playerUnit.stats.icea==true){
            if(EnemyUnit.stats.icea==true){
            Destroy(button);
            anim.SetInteger("State", 4);
            dialogueText.text = "IM AS DEAD AS KANYES CAREER";
            bText.text = "YOU GOT ICE!";
            }
        }

        if(playerUnit.stats.lightninga==true){
            if(EnemyUnit.stats.lightninga==true){
            Destroy(button);
            anim.SetInteger("State", 4);
            dialogueText.text = "YOU CAUGHT ME I'M BARACK OBAMA";
            bText.text = "YOU GOT THUNDER!";
            }
        }
    }

    // OnTriggerExit2D will check if the player is not colliding with the NPC and will deactivate the canvas.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.SetActive(false);
        }
    }
    
    // onstartbattlebutton will save the player's position, the player's stats and the enemy's stats and will send the player to the battle scene.
    public void onstartbattlebutton(){
        Transform posicion= Shaggy.GetComponent<Transform>();
        Vector3 actual = posicion.position;

                   
        PlayerPrefs.SetFloat("x", actual.x);
        PlayerPrefs.SetFloat("y", actual.y);
        unit Sh = Shaggy.GetComponent<unit>();
        string savedShaggy=JsonUtility.ToJson(Sh.stats);
        PlayerPrefs.SetString("Shaggy", savedShaggy);
        unit En = Enemy.GetComponent<unit>();
        PlayerPrefs.SetInt("Enemy", En.stats.index);
        PlayerPrefs.SetInt("Number", En.stats.number);
        SceneManager.LoadScene("Bosque_Combate");
    }
}
