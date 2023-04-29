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

    private void Start()
    {
        canvas.SetActive(false);
        EnemyUnit=Enemy.GetComponent<unit>();
    }

    void Update(){
        

        int enemyID=PlayerPrefs.GetInt("Dead");
        int Number=PlayerPrefs.GetInt("Number");
  
         if (enemyID == 1 && EnemyUnit.stats.number == Number)
         {
                anim.SetInteger("State", 4);
                
                Destroy(button);
                if (EnemyUnit.stats.number == 5)
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
