using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

public float time;
private float etime;
public float update;

public GameObject playerPrefab;
public GameObject enemyPrefab;
private Animator animatore;
private Animator animators;
public Transform playerBattleStation;
public Transform enemyBattleStation;
public Transform playerattack;
public Transform enemyattack;

unit playerUnit;
unit enemyUnit;


 public BattleHUD playerHUD;
 public BattleHUD enemyHUD;

 public TextMeshProUGUI dialogueText;


public BattleState state;

// Start is called before the first frame update
void Start()
{
    
    state = BattleState.START;
    StartCoroutine(SetupBattle());
}

IEnumerator SetupBattle()
{
    GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
    playerUnit = playerGO.GetComponent<unit>();

    GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
    enemyUnit = enemyGO.GetComponent<unit>();

    animators = playerGO.GetComponentInChildren<Animator>();
    animatore = enemyGO.GetComponentInChildren<Animator>();

  
    dialogueText.text = "A minion of the ZAZA  " + enemyUnit.unitName + " approaches...";

    enemyHUD.SetHUD(enemyUnit);
    playerHUD.SetHUD(playerUnit);
    
     yield return new WaitForSeconds(2f);

     state = BattleState.PLAYERTURN;
     PlayerTurn();
}

 IEnumerator PlayerAttack()
 {
     bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        if (isDead)
        {
            state = BattleState.WON;
            enemyHUD.SetHP(enemyUnit.currentHP = 0);
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;

            

            animators.SetInteger("State", 1);
            while(etime < time){
                yield return new WaitForSeconds(update);
                float interpolationRatio = etime / time;
                playerUnit.transform.position=  Vector3.Lerp(playerBattleStation.position, playerattack.position, interpolationRatio);
                etime += update;
            }

            animators.SetInteger("State", 2);
            yield return new WaitForSeconds(2f);
            dialogueText.text = "You deal " + playerUnit.damage + " damage...";
            enemyHUD.SetHP(enemyUnit.currentHP);
            animators.SetInteger("State", 3);
            while(etime > 0){
                yield return new WaitForSeconds(update);
                float interpolationRatio = etime / time;

                playerUnit.transform.position=  Vector3.Lerp(playerBattleStation.position, playerattack.position, interpolationRatio);
                etime -= update;
                
            }

            animators.SetInteger("State", 0);
            
            yield return new WaitForSeconds(1f);
            
            
            StartCoroutine(EnemyTurn());
        }
  
 }

 IEnumerator EnemyTurn()
 {
     animatore.SetInteger("State", 1);
     while(etime < time){
                yield return new WaitForSeconds(update);
                float interpolationRatio = etime / time;

                enemyUnit.transform.position=  Vector3.Lerp(enemyBattleStation.position, enemyattack.position, interpolationRatio);
                etime += update;
                
            }
     
     
     animatore.SetInteger("State", 2);
     dialogueText.text = enemyUnit.unitName + " attacks!";
     yield return new WaitForSeconds(2f);
     
     
     animatore.SetInteger("State", 3);
     
     while(etime > 0){
                yield return new WaitForSeconds(update);
                float interpolationRatio = etime / time;

                enemyUnit.transform.position=  Vector3.Lerp(enemyBattleStation.position, enemyattack.position, interpolationRatio);
                etime -= update;
                
            }
     
     
     animatore.SetInteger("State", 0);
     yield return new WaitForSeconds(1f);

     

     bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

     playerHUD.SetHP(playerUnit.currentHP);

     yield return new WaitForSeconds(1f);

     if(isDead)
     {
         state = BattleState.LOST;
         EndBattle();
     } else
     {
         state = BattleState.PLAYERTURN;
         PlayerTurn();
     }

 }

 void EndBattle()
 {
     if(state == BattleState.WON)
     {
        animatore.SetInteger("State", 4);
         dialogueText.text = "You won the battle!";
     } else if (state == BattleState.LOST)
     {
        animators.SetInteger("State", 5);
         dialogueText.text = "You were defeated.";
     }
 }

 void PlayerTurn()
 {
     dialogueText.text = "Choose an action:";
 }

 IEnumerator PlayerHeal()
 {
        playerUnit.Heal(5);

        state = BattleState.ENEMYTURN;
  
        animators.SetInteger("State", 4);
        yield return new WaitForSeconds(1f);
        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You healed for 5.";
        yield return new WaitForSeconds(1f);
        animators.SetInteger("State", 0);
        

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTurn());
  }

public void OnAttackButton()
 {
     if (state != BattleState.PLAYERTURN)
         return;

     StartCoroutine(PlayerAttack());
 }

 public void OnHealButton()
 {
     if (state != BattleState.PLAYERTURN)
         return;

     StartCoroutine(PlayerHeal());
 }
  }
