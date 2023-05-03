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
public int damagemade=0;
public int damagereceived=0;
public int coinsreceived=0;
public int misses=0;
public int crits=0;
public int melee=0;
public int iceuses=0;
public int fireuses=0;
public int thunderuses=0;
public int healuses=0;
public int rechargeuses=0;
public int result=0;
public string damagemades;
public string damagereceiveds;
public string coinsreceiveds;
public string missess;
public string critss;
public string melees;
public string iceusess;
public string fireusess;
public string thunderusess;
public string healusess;
public string rechargeusess;
public string results;

public GameObject playerPrefab;
public GameObject [] enemyPrefabs;


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
public Button Esc;
public Button fire;
public Button ice;
public Button thunder;

public Camera cam;

public Color color1 = Color.red;
public Color color2 = Color.blue;
public Color color3 = Color.yellow;
public Color color4 = Color.green;
public Color color5 = Color.black;

public float duration = 3.0F;

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



    state = BattleState.START;
    StartCoroutine(SetupBattle());
}

IEnumerator SetupBattle()
{
    
    attackButton.interactable = false;
    elementButton.interactable = false;
    healButton.interactable = false;
    fleeButton.interactable = false;
    PlayerPrefs.SetInt("Dead", 0);

    

    

    GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
    
    playerUnit = playerGO.GetComponent<unit>();
    string save=PlayerPrefs.GetString("Shaggy");
    playerUnit.stats= JsonUtility.FromJson<Stats>(save);
    //playersprite= playerGO.GetComponent<SpriteRenderer>();
     
    
    int enemyID=PlayerPrefs.GetInt("Enemy");
    GameObject enemyGO = Instantiate(enemyPrefabs[enemyID], enemyBattleStation);
    
    enemyUnit= enemyGO.GetComponent<unit>();
    
    //enemysprite= enemyGO.GetComponent<SpriteRenderer>();

    animators = playerGO.GetComponentInChildren<Animator>();
    animatore = enemyGO.GetComponentInChildren<Animator>();
    animatore.SetInteger("State", 0);

    if(enemyUnit.stats.index==1){
        cam.backgroundColor = color1;
    }

    else if(enemyUnit.stats.index==2){
        cam.backgroundColor = color2;
    }

    else if(enemyUnit.stats.index==3){
        cam.backgroundColor = color3;
    }

    else if(enemyUnit.stats.index==4){
        cam.backgroundColor = color4;
    }

    else if(enemyUnit.stats.index==5 || enemyUnit.stats.index==6){
        cam.backgroundColor = color5;
    }

    dialogueText.text = "A minion of the ZAZA  " + enemyUnit.unitName + " approaches...";

    enemyHUD.SetHUD(enemyUnit);
    playerHUD.SetHUD(playerUnit);
    playerHUD.SetMP(playerUnit.stats.currentMP);
    
     yield return new WaitForSeconds(2f);

     state = BattleState.PLAYERTURN;
     PlayerTurn();
     
}

 IEnumerator PlayerAttack(){
    state = BattleState.ENEMYTURN;
    System.Random rand = new System.Random();
    int number = rand.Next(0, 100);
    if (number < (25-playerUnit.stats.accuracy+enemyUnit.stats.agility)){
            dialogueText.text = playerUnit.unitName + " Failed to attack";
            misses++;
            yield return new WaitForSeconds(1f);
            if (playerUnit.stats.lightning>0){
                yield return new WaitForSeconds(1f);
                playerUnit.setlightning(0);
                playerUnit.lightningnerf();
                dialogueText.text = playerUnit.unitName + " Stats returned to normal";
                Thunders.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
            StartCoroutine(EnemyTurn());
        }
    
    else{
        int damages=0;
        int crit=0;
        
        getAttackplayer(ref damages, ref crits);
        bool isDead = enemyUnit.TakeDamage(damages);
    //enemysprite.color= Color.red;
        if (isDead){
            state = BattleState.WON;

            if (enemyUnit.stats.index==1){
                playerUnit.stats.firea=true;
                dialogueText.text = "You can now use Fire!";
                yield return new WaitForSeconds(1f);
            }
            if (enemyUnit.stats.index==2){
                playerUnit.stats.icea=true;
                dialogueText.text = "You can now use Ice!";
                yield return new WaitForSeconds(1f);
            }
            if (enemyUnit.stats.index==3){
                playerUnit.stats.lightninga=true;
                dialogueText.text = "You can now use thunder!";
                yield return new WaitForSeconds(1f);
            }

   

            enemyHUD.SetHP(enemyUnit.stats.currentHP = 0);
            dialogueText.text = "You have defeated " + enemyUnit.unitName + "!";
            animatore.SetInteger("State", 4);
            yield return new WaitForSeconds(1f);
            playerUnit.stats.coins= playerUnit.stats.coins+enemyUnit.stats.coins+playerUnit.stats.charisma;
            coinsreceived=enemyUnit.stats.coins+playerUnit.stats.charisma+coinsreceived;    
            dialogueText.text = "Now you have: " + playerUnit.stats.coins + " coins";
            yield return new WaitForSeconds(1f);
            enemyUnit.stats.dead = 1;
            PlayerPrefs.SetInt("Dead", enemyUnit.stats.dead);



            string savedShaggy=JsonUtility.ToJson(playerUnit.stats);
            PlayerPrefs.SetString("Shaggy", savedShaggy);

            if (enemyUnit.stats.index==5)
                {
                    cam.backgroundColor = color5;
                    dialogueText.text = "HAHAHAHAHAHA I HAVE TRICKED YOU";
                    yield return new WaitForSeconds(1f);

                    
                }

            EndBattle();
        }
        
            
        else{
        melee++;
        animators.SetInteger("State", 1);
            while(etime < time){
                yield return new WaitForSeconds(update);
                float interpolationRatio = etime / time;
                playerUnit.transform.position=  Vector3.Lerp(playerBattleStation.position, playerattack.position, interpolationRatio);
                etime += update;
            }

            animators.SetInteger("State", 2);
            if (crit==1){
                dialogueText.text = "Critical Hit";
                crits++;
            }
            else{
                dialogueText.text = "Attack Successful";
            }

            yield return new WaitForSeconds(1f);
            dialogueText.text = "Shaggy did " + damages + " damage!";
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
 }


 IEnumerator EnemyTurn(){
    System.Random rand = new System.Random();
    int number = rand.Next(0, 100);
    if (number < (25-enemyUnit.stats.accuracy+playerUnit.stats.agility)){
        dialogueText.text = enemyUnit.unitName+ " Failed to attack";
            yield return new WaitForSeconds(1f);
            if (playerUnit.stats.lightning>0){
                yield return new WaitForSeconds(1f);
                playerUnit.setlightning(0);
                playerUnit.lightningnerf();
                dialogueText.text = playerUnit.unitName + " Stats returned to normal";
                Thunders.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                
            }
            
            if (playerUnit.stats.fire > 0){
            enemyUnit.TakeDamage(2);
            enemyUnit.decreasefire();
            dialogueText.text = "Took 2 fire damage from fire";
            enemyHUD.SetHP(enemyUnit.stats.currentHP);
            yield return new WaitForSeconds(1f);
            }
        state = BattleState.PLAYERTURN;
        PlayerTurn();   
    }

    else{

    if (playerUnit.stats.fire > 0){
            enemyUnit.TakeDamage(2);
            enemyUnit.decreasefire();
            dialogueText.text = "Took 2 fire damage from fire";
            enemyHUD.SetHP(enemyUnit.stats.currentHP);
            yield return new WaitForSeconds(1f);
            }
    else{
        Firee.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    if (enemyUnit.stats.ice == 0){
        int damagee=0;
        int crite=0;
        animatore.SetInteger("State", 1);
        while(etime < time){
            yield return new WaitForSeconds(update);
            float interpolationRatio = etime / time;
            enemyUnit.transform.position=  Vector3.Lerp(enemyBattleStation.position, enemyattack.position, interpolationRatio);
            etime += update;
                
        }
       
        animatore.SetInteger("State", 2);
        dialogueText.text = enemyUnit.unitName + " attacks!";
        
        if (enemyUnit.stats.firea==true){
            System.Random rand1 = new System.Random();
            int number1 = rand1.Next(0, 100);
            if (number1 < 50){
                yield return new WaitForSeconds(1f);
                playerUnit.setfire();
                Fires.Play(true);
                dialogueText.text = "Enemy used fire! you will take 2 damage for 3 turns";
            }
        }

        if (enemyUnit.stats.icea==true){
            System.Random rand2 = new System.Random();
            int number2 = rand2.Next(0, 100);
            if (number2 < 35){
                yield return new WaitForSeconds(1f);
                playerUnit.setice(2);
                Ices.Play(true);
                dialogueText.text = "Enemy used ice! You are now frozen for 1 turn";
            }
            
        }

        if (enemyUnit.stats.lightninga==true){
            System.Random rand3 = new System.Random();
            int number3 = rand3.Next(0, 100);
            if (number3 < 50){
                yield return new WaitForSeconds(1f);
                enemyUnit.setlightning(2);
                Thundere.Play(true);
                dialogueText.text = "Enemy used lightning! His attack will be buffed next turn";
            }
        }

        yield return new WaitForSeconds(1f);
        getAttackenemy(ref damagee, ref crite);
        if(crite==1){
            dialogueText.text = "Critical Hit";
            yield return new WaitForSeconds(1f);
        }
        else{
            dialogueText.text = "Attack Successful";
            yield return new WaitForSeconds(1f);
        }    
        dialogueText.text = "Enemy dealt " + damagee + " damage!";
        bool isDead = playerUnit.TakeDamage(damagee);

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

            if(enemyUnit.stats.lightninga==true){

            }
            
            if (enemyUnit.stats.lightning > 0 ){
                enemyUnit.setlightning(enemyUnit.stats.lightning-1);
                if(enemyUnit.stats.lightning == 0 ){
                    enemyUnit.lightningnerf();
                dialogueText.text = enemyUnit.unitName + " Stats returned to normal";
                Thundere.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                yield return new WaitForSeconds(1f);
                }
            }

            
            
            if(isDead){
                state = BattleState.LOST;
                EndBattle();
            } 

            else{
                state = BattleState.PLAYERTURN;
                PlayerTurn();
                
            }
        }
        
        else{
            enemyUnit.decreaseice();
            Icee.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            state = BattleState.PLAYERTURN;
            PlayerTurn();
            
        }
    }  
}
    
    
 void EndBattle()
 {
     if(state == BattleState.WON)
     {
        result=1;
        attackButton.interactable = false;
        elementButton.interactable = false;
        healButton.interactable = false;
        fleeButton.interactable = false;

        damagemades=PlayerPrefs.GetString("Damagemade");
        damagereceiveds=PlayerPrefs.GetString("Damagereceived");
        coinsreceiveds=PlayerPrefs.GetString("Coinsmade");
        missess=PlayerPrefs.GetString("Misses");
        critss=PlayerPrefs.GetString("Crits");
        melees=PlayerPrefs.GetString("Melee");
        iceusess=PlayerPrefs.GetString("Ice");
        fireusess=PlayerPrefs.GetString("Fire");
        thunderusess=PlayerPrefs.GetString("Lightning");
        healusess=PlayerPrefs.GetString("Heal");
        rechargeusess=PlayerPrefs.GetString("Recharge");
        results=PlayerPrefs.GetString("Result");

        PlayerPrefs.SetString("Coinsmade", coinsreceiveds + "|" + coinsreceived.ToString());
        PlayerPrefs.SetString("Damagemade", damagemades  + "|" + damagemade.ToString());
        PlayerPrefs.SetString("Damagereceived", damagereceiveds + "|" + damagereceived.ToString());
        PlayerPrefs.SetString("Misses", missess + "|" + misses.ToString());
        PlayerPrefs.SetString("Crits", critss + "|" + crits.ToString());
        PlayerPrefs.SetString("Melee", melees + "|" + melee.ToString());
        PlayerPrefs.SetString("Ice",iceusess + "|" + iceuses.ToString());
        PlayerPrefs.SetString("Fire", fireusess + "|" + fireuses.ToString());
        PlayerPrefs.SetString("Lightning",thunderusess + "|" + thunderuses.ToString());
        PlayerPrefs.SetString("Heal", healusess + "|" + healuses.ToString());
        PlayerPrefs.SetString("Recharge", rechargeusess + "|" + rechargeuses.ToString());
        PlayerPrefs.SetString("Result", results + "|" + result.ToString());
        PlayerPrefs.Save();


       if (enemyUnit.stats.index==5){
            SceneManager.LoadScene("FinalBoss");        
       }

        else if (enemyUnit.stats.index==6){
            SceneManager.LoadScene("cutscene2");            
        }

        else if (enemyUnit.stats.index==4){
            SceneManager.LoadScene("Torre");
        }

        else{
        SceneManager.LoadScene("TileMap");
        }
     } 
     else if (state == BattleState.LOST){
        
        animators.SetInteger("State", 5);
        dialogueText.text = "You were defeated. Flee or Restart";
        attackButton.interactable = false;
        elementButton.interactable = false;
        healButton.interactable = false;
        fleeButton.interactable = true;
        playerUnit.stats.currentHP=1;
     }
 }

 void PlayerTurn()
 {
    if(playerUnit.stats.ice==0){
    attackButton.interactable = true;
    elementButton.interactable = true;
    healButton.interactable = true;
    fleeButton.interactable = true;

    if(enemyUnit.stats.index==6){
        Esc.interactable = false;
    }

    if(playerUnit.stats.lightninga==false){
        thunder.interactable = false;
    }

    else{
        thunder.interactable = true;
    }

    if(playerUnit.stats.firea == false){
        fire.interactable = false;
    }
    
        else{
            fire.interactable = true;
        }

    if(playerUnit.stats.icea == false){
        ice.interactable = false;
    }

    else{
        ice.interactable = true;
    }
    

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
        attackButton.interactable = false;
        elementButton.interactable = false;
        healButton.interactable = false;
        fleeButton.interactable = false;
        StartCoroutine(EnemyTurn());
        }
     
     Ices.Stop(true, ParticleSystemStopBehavior.StopEmitting);

     playerHUD.SetHP(playerUnit.stats.currentHP);

     dialogueText.text = "Choose an action:";
    }

    else if (playerUnit.stats.ice>0){
        playerUnit.setice(0);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
 }



 IEnumerator PlayerRecharge()
 {
        rechargeuses++;
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
        healuses++;
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
        fireuses=fireuses+1;
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
        iceuses=iceuses+1;                  
        System.Random rand = new System.Random();
        int number = rand.Next(0, 100);
        
        if(number < 20)
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
        thunderuses=thunderuses+1;
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

    if (playerUnit.stats.currentMP>=10){
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
        SceneManager.LoadScene("Bosque_Combate");
    }
    
    public void Escape(){
        damagemades=PlayerPrefs.GetString("Damagemade");
        damagereceiveds=PlayerPrefs.GetString("Damagereceived");
        coinsreceiveds=PlayerPrefs.GetString("Coinsmade");
        missess=PlayerPrefs.GetString("Misses");
        critss=PlayerPrefs.GetString("Crits");
        melees=PlayerPrefs.GetString("Melee");
        iceusess=PlayerPrefs.GetString("Ice");
        fireusess=PlayerPrefs.GetString("Fire");
        thunderusess=PlayerPrefs.GetString("Lightning");
        healusess=PlayerPrefs.GetString("Heal");
        rechargeusess=PlayerPrefs.GetString("Recharge");
        results=PlayerPrefs.GetString("Result");



        PlayerPrefs.SetString("Coinsmade", coinsreceiveds + "|" + coinsreceived.ToString());
        PlayerPrefs.SetString("Damagemade", damagemades  + "|" + damagemade.ToString());
        PlayerPrefs.SetString("Damagereceived", damagereceiveds + "|" + damagereceived.ToString());
        PlayerPrefs.SetString("Misses", missess + "|" + misses.ToString());
        PlayerPrefs.SetString("Crits", critss + "|" + crits.ToString());
        PlayerPrefs.SetString("Melee", melees + "|" + melee.ToString());
        PlayerPrefs.SetString("Ice",iceusess + "|" + iceuses.ToString());
        PlayerPrefs.SetString("Fire", fireusess + "|" + fireuses.ToString());
        PlayerPrefs.SetString("Lightning",thunderusess + "|" + thunderuses.ToString());
        PlayerPrefs.SetString("Heal", healusess + "|" + healuses.ToString());
        PlayerPrefs.SetString("Recharge", rechargeusess + "|" + rechargeuses.ToString());
        PlayerPrefs.SetString("Result", results + "|" + result.ToString());
        PlayerPrefs.Save();
        System.Random rand = new System.Random();
        int lostcoins= rand.Next(0,5);
        dialogueText.text = "You escaped and lost "+lostcoins+" coins";
        //FIX SO THAT TEXT SHOWS CORRECTLY
        playerUnit.stats.coins-=lostcoins;
        
        
        int place=PlayerPrefs.GetInt("place");
        

        string savedShaggy=JsonUtility.ToJson(playerUnit.stats);
        PlayerPrefs.SetString("Shaggy", savedShaggy);
        if(place==2){    
        PlayerPrefs.SetFloat("x", Convert.ToSingle(-3.9));
        PlayerPrefs.SetFloat("y", Convert.ToSingle(3.5));
        SceneManager.LoadScene("Torre");

        }

        else if(place==1){
        PlayerPrefs.SetFloat("x", Convert.ToSingle(-10.4));
        PlayerPrefs.SetFloat("y", Convert.ToSingle(1.5));
        SceneManager.LoadScene("TileMap");
        
        }
        
    }

    public void getAttackplayer(ref int damages, ref int crit){
        crit=0;
        System.Random rand = new System.Random();
        int attack = rand.Next(5, 10)+playerUnit.stats.damage;
        int defence = rand.Next(-3, 0)+enemyUnit.stats.defence;
        damages=attack-defence;
        
        if (damages<=0){
            damages=1;
        }
        
        int number = rand.Next(0, 100);
        
        if(number < 20 + playerUnit.stats.luck)
        {   


            damages=damages*2;
            crit=1;
        }
        damagemade=damages+damagemade;

    }

    public void getAttackenemy(ref int damagee, ref int crite){
        crite=0;
        System.Random rand = new System.Random();
        int attack = rand.Next(5, 10)+enemyUnit.stats.damage;
        int defence = rand.Next(-3, 0)+playerUnit.stats.defence;
        damagee=attack-defence;
        
        if (damagee<=0){
            damagee=1;
        }
        
        int number = rand.Next(0, 100);
        
        if(number < 20 + playerUnit.stats.luck)
        {   


            damagee=damagee*2;
            crite=1;
        }
        damagereceived=damagee+damagereceived;
    }
}
