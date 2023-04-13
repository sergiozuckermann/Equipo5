using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

public GameObject playerPrefab;
public GameObject enemyPrefab;

public Transform playerBattleStation;
public Transform enemyBattleStation;

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
            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You deal " + playerUnit.damage + " damage...";

            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }
  
 }

 IEnumerator EnemyTurn()
 {
     
     dialogueText.text = enemyUnit.unitName + " attacks!";

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
         dialogueText.text = "You won the battle!";
     } else if (state == BattleState.LOST)
     {
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

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You healed for 5.";

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
