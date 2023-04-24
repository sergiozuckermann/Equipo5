using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;


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

public ParticleSystem Fires;
public ParticleSystem Thunders;
public ParticleSystem Ices;
public ParticleSystem Heals;
public ParticleSystem Recharges;
public ParticleSystem Firee;
public ParticleSystem Thundere;
public ParticleSystem Icee;

public Button attackButton;
public Button elementButton;
public Button healButton;
public Button fleeButton;

unit playerUnit;
unit enemyUnit;

SpriteRenderer playersprite;
SpriteRenderer enemysprite;



 public BattleHUD playerHUD;
 public BattleHUD enemyHUD;

 public TextMeshProUGUI dialogueText;


public BattleState state;

// Start is called before the first frame update
void Start()
{
    Fires.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    Ices.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    Thunders.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    Firee.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    Icee.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    Thundere.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    Heals.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    Recharges.Stop(true, ParticleSystemStopBehavior.StopEmitting);

    attackButton.interactable = false;
    elementButton.interactable = false;
    healButton.interactable = false;
    fleeButton.interactable = false;

    state = BattleState.START;
    StartCoroutine(SetupBattle());
}

IEnumerator SetupBattle()
{
    

    GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
    
    playerUnit = playerGO.GetComponent<unit>();
    string save=PlayerPrefs.GetString("Shaggy");
    playerUnit.stats= JsonUtility.FromJson<Stats>(save);
    //playersprite= playerGO.GetComponent<SpriteRenderer>();
     
    
    GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
    enemyUnit = enemyGO.GetComponent<unit>();
    //enemysprite= enemyGO.GetComponent<SpriteRenderer>();

    animators = playerGO.GetComponentInChildren<Animator>();
    animatore = enemyGO.GetComponentInChildren<Animator>();

    
    dialogueText.text = "A minion of the ZAZA  " + enemyUnit.unitName + " approaches...";

    enemyHUD.SetHUD(enemyUnit);
    playerHUD.SetHUD(playerUnit);
    playerHUD.SetMP(playerUnit.stats.currentMP);
    
     yield return new WaitForSeconds(2f);

     state = BattleState.PLAYERTURN;
     PlayerTurn();
     
}

 IEnumerator PlayerAttack()
 {
    

     bool isDead = enemyUnit.TakeDamage(playerUnit.stats.damage);
     //enemysprite.color= Color.red;
     
        

        if (isDead)
        {
            state = BattleState.WON;
            string savedShaggy=JsonUtility.ToJson(playerUnit.stats);
            PlayerPrefs.SetString("Shaggy", savedShaggy);
           
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
            dialogueText.text = "You deal " + playerUnit.stats.damage + " damage...";
            
            

            enemyHUD.SetHP(enemyUnit.stats.currentHP);
            animators.SetInteger("State", 3);
            
            while(etime > 0){
                yield return new WaitForSeconds(update);
                float interpolationRatio = etime / time;
                playerUnit.transform.position=  Vector3.Lerp(playerBattleStation.position, playerattack.position, interpolationRatio);
                etime -= update;
            }

            if (playerUnit.stats.lightning>0){
            yield return new WaitForSeconds(1f);
            playerUnit.setlightning(0);
            playerUnit.lightningnerf();
            dialogueText.text = playerUnit.unitName + " Stats returned to normal";
            Thunders.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

            animators.SetInteger("State", 0);
            
            yield return new WaitForSeconds(1f);
            StartCoroutine(EnemyTurn());
        }
 }

 IEnumerator EnemyTurn()
 {
    if (enemyUnit.stats.ice == 0){
         playerUnit.setice(0);
         if (enemyUnit.stats.fire > 0){
            enemyUnit.TakeDamage(2);
            enemyUnit.decreasefire();
            dialogueText.text = enemyUnit.unitName + " Took 2 damage from fire " + enemyUnit.stats.fire + " turns remaining";
            enemyHUD.SetHUD(enemyUnit);
            yield return new WaitForSeconds(1f);
        }
        else{
            Firee.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }


    enemyHUD.SetHP(enemyUnit.stats.currentHP);

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
     dialogueText.text = "Enemy dealt " + enemyUnit.stats.damage + " damage...";
     bool isDead = playerUnit.TakeDamage(enemyUnit.stats.damage);

    if (playerUnit.stats.currentHP>0){
        playerHUD.SetHP(playerUnit.stats.currentHP);
    }
    else{
        playerHUD.SetHP(0);
    }
     
     animatore.SetInteger("State", 3);
     
     while(etime > 0){
                yield return new WaitForSeconds(update);
                float interpolationRatio = etime / time;

                enemyUnit.transform.position=  Vector3.Lerp(enemyBattleStation.position, enemyattack.position, interpolationRatio);
                etime -= update;
                
            }
     
     
     animatore.SetInteger("State", 0);
     yield return new WaitForSeconds(1f);

     

     

     yield return new WaitForSeconds(1f);
    if (enemyUnit.stats.lightning >0 ){
        enemyUnit.setlightning(0);
        enemyUnit.lightningnerf();
        dialogueText.text = enemyUnit.unitName + " Stats returned to normal";
        Thundere.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        yield return new WaitForSeconds(1f);
     }
     if(isDead)
     {
         state = BattleState.LOST;
         EndBattle();
     } else
     {
         state = BattleState.PLAYERTURN;
         if (playerUnit.stats.fire < 0){
            dialogueText.text = "Took 2 fire damage from fire";
            yield return new WaitForSeconds(1f);
        }
         PlayerTurn();
        

     }
    }
    
    else
     {
        enemyUnit.decreaseice();
        state = BattleState.PLAYERTURN;
        PlayerTurn();
        Icee.Stop(true, ParticleSystemStopBehavior.StopEmitting);
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
         attackButton.interactable = false;
        elementButton.interactable = false;
        healButton.interactable = false;
        fleeButton.interactable = false;
     }
 }

 void PlayerTurn()
 {
    attackButton.interactable = true;
    elementButton.interactable = true;
    healButton.interactable = true;
    fleeButton.interactable = true;

     if (playerUnit.stats.fire > 0){
        playerUnit.TakeDamage(2);
        playerUnit.decreasefire();
        dialogueText.text = playerUnit.unitName + " Took 2 damage from fire" + playerUnit.stats.fire + "turns remaining";
        playerHUD.SetHP(playerUnit.stats.currentHP);
        
     }

     else{
        Fires.Stop(true, ParticleSystemStopBehavior.StopEmitting);
     }
     
      if (playerUnit.stats.ice > 0){
         playerUnit.setice(0);
         state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        }
     
     Ices.Stop(true, ParticleSystemStopBehavior.StopEmitting);

     playerHUD.SetHP(playerUnit.stats.currentHP);

     dialogueText.text = "Choose an action:";
 }



 IEnumerator PlayerRecharge()
 {
        playerUnit.Recharge(5);

        state = BattleState.ENEMYTURN;
        Recharges.Play();

        animators.SetInteger("State", 4);
        yield return new WaitForSeconds(1f);
        playerHUD.SetMP(playerUnit.stats.currentMP);
        dialogueText.text = "You recharged 5 MP.";
        yield return new WaitForSeconds(1f);
        animators.SetInteger("State", 0);
        Recharges.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTurn());
  }

   IEnumerator PlayerHeal()
 {
        playerUnit.Heal(10);

        state = BattleState.ENEMYTURN;
        Heals.Play();
        animators.SetInteger("State", 4);
        yield return new WaitForSeconds(1f);
        playerHUD.SetHP(playerUnit.stats.currentHP);
        dialogueText.text = "You healed for 10.";
        yield return new WaitForSeconds(1f);
        animators.SetInteger("State", 0);
        Heals.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        

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
            Firee.Play();
            dialogueText.text = "Enemy is now burning! Will take damage every turn for " + enemyUnit.stats.fire + " turns";
           yield return new WaitForSeconds(1f);
            dialogueText.text = "Enemy 2 damage from fire";
            enemyUnit.TakeDamage(2);
            enemyUnit.decreasefire();
            enemyHUD.SetHP(enemyUnit.stats.currentHP);
            
        }
        else
        {
            playerUnit.setfire();
            dialogueText.text = "You are burning! Will take damage every turn for " + playerUnit.stats.fire + " turns remaining";
            Fires.Play();
            playerUnit.TakeDamage(2);   
            playerUnit.decreasefire();
            yield return new WaitForSeconds(1f);
            dialogueText.text = "Took 2 damage from fire";

            playerHUD.SetHP(playerUnit.stats.currentHP);
        }
        


        state = BattleState.ENEMYTURN;
  
        animators.SetInteger("State", 4);
        yield return new WaitForSeconds(1f);
        playerHUD.SetHP(playerUnit.stats.currentHP);
        enemyHUD.SetHP(enemyUnit.stats.currentHP);

       
        
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
            Icee.Play();

        }
        else
        {
            playerUnit.setice(1);
            dialogueText.text = "You are frozen! Will lose next turn";
            Ices.Play();

        }
        


        state = BattleState.ENEMYTURN;
  
        animators.SetInteger("State", 4);
        yield return new WaitForSeconds(1f);
        playerHUD.SetHP(playerUnit.stats.currentHP);
        enemyHUD.SetHP(enemyUnit.stats.currentHP);

       
        
        yield return new WaitForSeconds(1f);
        animators.SetInteger("State", 0);
        

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTurn());
  }

 IEnumerator PlayerLight()
 {
        
        System.Random rand = new System.Random();
        int number = rand.Next(0, 100);
        
        if(number < 20)
        {   
            enemyUnit.setlightning(1);
            Thundere.Play();
            dialogueText.text = "Enemy was hit by lighting, double damage next attack";
            enemyUnit.lightningbuff();

        }

        else
        {
            playerUnit.setlightning(1);
            Thunders.Play();

            dialogueText.text = "You got hit by lighting, double damage next attack";
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
    attackButton.interactable = false;
    elementButton.interactable = false;
    healButton.interactable = false;
    fleeButton.interactable = false;
     StartCoroutine(PlayerAttack());
 }

 public void OnHealButton()
 {
     if (state != BattleState.PLAYERTURN)
         return;
    attackButton.interactable = false;
    elementButton.interactable = false;
    healButton.interactable = false;
    fleeButton.interactable = false;
     
     if (playerUnit.stats.currentMP>=5){
        attackButton.interactable = false;
        elementButton.interactable = false;
        healButton.interactable = false;
        fleeButton.interactable = false;
        playerUnit.setmp(playerUnit.stats.currentMP - 5);
        playerHUD.SetMP(playerUnit.stats.currentMP);
        StartCoroutine(PlayerHeal());
        
    }
     
    else{
        dialogueText.text = "Not enough MP";
        return;
    }
 }

  public void OnRechargeButton()
 {
     if (state != BattleState.PLAYERTURN)
         return;
    attackButton.interactable = false;
    elementButton.interactable = false;
    healButton.interactable = false;
    fleeButton.interactable = false;
     StartCoroutine(PlayerRecharge());
 }

  public void OnFireButton()
    {
         if (state != BattleState.PLAYERTURN)
         return;

    if (playerUnit.stats.currentMP>=10){
        attackButton.interactable = false;
        elementButton.interactable = false;
        healButton.interactable = false;
        fleeButton.interactable = false;        
        playerUnit.setmp(playerUnit.stats.currentMP - 10);
        playerHUD.SetMP(playerUnit.stats.currentMP);
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

    if (playerUnit.stats.currentMP>=5){
        attackButton.interactable = false;
        elementButton.interactable = false;
        healButton.interactable = false;
        fleeButton.interactable = false;
        playerUnit.setmp(playerUnit.stats.currentMP - 5);
        playerHUD.SetMP(playerUnit.stats.currentMP);
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

     if (playerUnit.stats.currentMP>=10){
        attackButton.interactable = false;
        elementButton.interactable = false;
        healButton.interactable = false;
        fleeButton.interactable = false;
        playerUnit.setmp(playerUnit.stats.currentMP - 10);
        playerHUD.SetMP(playerUnit.stats.currentMP);
        StartCoroutine(PlayerLight());
    } 
    else{
            dialogueText.text = "Not enough MP";
            return;
        }
    }

    public void RestartLevel(){
        SceneManager.LoadScene(2);
    }
    
    public void Escape(){
        string savedShaggy=JsonUtility.ToJson(playerUnit.stats);
        PlayerPrefs.SetString("Shaggy", savedShaggy);
        SceneManager.LoadScene("Bosque_Combate");
        SceneManager.LoadScene(1);
    }
}
