using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


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
    playerHUD.SetMP(playerUnit.currentMP);
    
     yield return new WaitForSeconds(2f);

     state = BattleState.PLAYERTURN;
     PlayerTurn();
}

 IEnumerator PlayerAttack()
 {
     bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
     
     if (playerUnit.lightning>0){
        playerUnit.setlightning(0);
        playerUnit.lightningnerf();
        dialogueText.text = playerUnit.unitName + " Stats returned to normal";
        yield return new WaitForSeconds(1f);
     }

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
    if (enemyUnit.ice == 0){
         playerUnit.setice(0);
         if (enemyUnit.fire > 0){
     enemyUnit.TakeDamage(2);
     enemyUnit.decreasefire();
     dialogueText.text = enemyUnit.unitName + " Took fire damage " + enemyUnit.fire + " turns remaining";
     }


    enemyHUD.SetHP(enemyUnit.currentHP);

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
    if (enemyUnit.lightning >0 ){
        enemyUnit.setlightning(0);
        enemyUnit.lightningnerf();
        dialogueText.text = enemyUnit.unitName + " Stats returned to normal";
        yield return new WaitForSeconds(1f);
     }
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
    
    else
     {
        enemyUnit.decreaseice();
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
     if (playerUnit.fire > 0){
     playerUnit.TakeDamage(2);
     playerUnit.decreasefire();
     dialogueText.text = playerUnit.unitName + " Took fire damage" + playerUnit.fire + "turns remaining";
     enemyHUD.SetHP(enemyUnit.currentHP);
     }
     
      if (playerUnit.ice > 0){
         playerUnit.setice(0);
         state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        }

     playerHUD.SetHP(playerUnit.currentHP);

     dialogueText.text = "Choose an action:";
 }

 IEnumerator PlayerRecharge()
 {
        playerUnit.Recharge(5);

        state = BattleState.ENEMYTURN;
  
        animators.SetInteger("State", 4);
        yield return new WaitForSeconds(1f);
        playerHUD.SetMP(playerUnit.currentMP);
        dialogueText.text = "You recharged 5 MP.";
        yield return new WaitForSeconds(1f);
        animators.SetInteger("State", 0);
        

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTurn());
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

   IEnumerator PlayerFire()
 {
        
        System.Random rand = new System.Random();
        int number = rand.Next(0, 100);
        
        if(number < 70)
        {
            
            enemyUnit.setfire();
            dialogueText.text = "Enemy is now burning! Will take damage every turn for " + enemyUnit.fire + " turns";
            enemyUnit.TakeDamage(2);
            enemyUnit.decreasefire();
            playerHUD.SetHP(playerUnit.currentHP);
        }
        else
        {
            playerUnit.setfire();
            dialogueText.text = "You are burning! Will take damage every turn for " + playerUnit.fire + " turns remaining";

            playerUnit.TakeDamage(2);   
            playerUnit.decreasefire();
            enemyHUD.SetHP(enemyUnit.currentHP);
        }
        


        state = BattleState.ENEMYTURN;
  
        animators.SetInteger("State", 4);
        yield return new WaitForSeconds(1f);
        playerHUD.SetHP(playerUnit.currentHP);
        enemyHUD.SetHP(enemyUnit.currentHP);

       
        
        yield return new WaitForSeconds(1f);
        animators.SetInteger("State", 0);
        

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTurn());
  }

   IEnumerator PlayerIce()
 {
        
        System.Random rand = new System.Random();
        int number = rand.Next(0, 100);
        
        if(number < 50)
        {   
            enemyUnit.setice(2);
            dialogueText.text = "Enemy is now frozen! Will lose next turn";
        }
        else
        {
            playerUnit.setice(1);
            dialogueText.text = "You are frozen! Will lose next turn";
          
        }
        


        state = BattleState.ENEMYTURN;
  
        animators.SetInteger("State", 4);
        yield return new WaitForSeconds(1f);
        playerHUD.SetHP(playerUnit.currentHP);
        enemyHUD.SetHP(enemyUnit.currentHP);

       
        
        yield return new WaitForSeconds(1f);
        animators.SetInteger("State", 0);
        

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTurn());
  }

 IEnumerator PlayerLight()
 {
        
        System.Random rand = new System.Random();
        int number = rand.Next(0, 100);
        
        if(number < 50)
        {   
            enemyUnit.setlightning(1);
            dialogueText.text = "Enemy was hit by lighting, stats buffed";
            enemyUnit.lightningbuff();

        }
        else
        {
            playerUnit.setlightning(1);
            dialogueText.text = "You hit by lighting, stats buffed";
            playerUnit.lightningbuff();
        }
        


        state = BattleState.ENEMYTURN;
  
        animators.SetInteger("State", 4);
        yield return new WaitForSeconds(1f);

       
        
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

  public void OnRechargeButton()
 {
     if (state != BattleState.PLAYERTURN)
         return;

     StartCoroutine(PlayerRecharge());
 }

  public void OnFireButton()
 {
         if (state != BattleState.PLAYERTURN)
         return;

    if (playerUnit.currentMP>=10){
        
        playerUnit.setmp(playerUnit.currentMP - 10);
        playerHUD.SetMP(playerUnit.currentMP);
        StartCoroutine(PlayerFire());
        
    }
     
    else{
        dialogueText.text = "Not enough MP";
        return;
    }
    
 }

   public void OnIceButton()
 {
     if (state != BattleState.PLAYERTURN)
         return;

    if (playerUnit.currentMP>=5){
        
        playerUnit.setmp(playerUnit.currentMP - 5);
        playerHUD.SetMP(playerUnit.currentMP);
        StartCoroutine(PlayerIce());
        
    }
     
    else{
        dialogueText.text = "Not enough MP";
        return;
    }
 }
 
 public void OnLightButton()
 {
     if (state != BattleState.PLAYERTURN)
         return;

     if (playerUnit.currentMP>=10){
        
        playerUnit.setmp(playerUnit.currentMP - 10);
        playerHUD.SetMP(playerUnit.currentMP);
        StartCoroutine(PlayerLight());
        
    }
     
    else{
        dialogueText.text = "Not enough MP";
        return;
    }
 }

  }
