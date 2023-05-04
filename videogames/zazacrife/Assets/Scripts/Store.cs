//Code by Zaza team
// Description: This script is used to control the store. It allows the player to buy upgrades to their stats and to heal and to upgrade.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{
public GameObject player;


public Button att;
public Button def;
public Button agl;
public Button lck;
public Button acc;
public Button chr;
public Button HP;
public Button MP;
public Button Recharge;
public Button heal;

unit playerUnit;

public TextMeshProUGUI dialogueText;
public TextMeshProUGUI Attack;
public TextMeshProUGUI Defence;
public TextMeshProUGUI Agility;
public TextMeshProUGUI Luck;
public TextMeshProUGUI Accuracy;
public TextMeshProUGUI Charisma;
public TextMeshProUGUI HPText;
public TextMeshProUGUI MPText;
public TextMeshProUGUI coins;

    //The start function is used to set the player unit and to set the buttons to their respective functions
    public void Start()
    {
        playerUnit = player.GetComponent<unit>();

        att.onClick.AddListener(Att);
        def.onClick.AddListener(Def);
        agl.onClick.AddListener(Agl);
        lck.onClick.AddListener(Lck);
        acc.onClick.AddListener(Acc);
        chr.onClick.AddListener(Chr);
        HP.onClick.AddListener(hp);
        MP.onClick.AddListener(mp);
        Recharge.onClick.AddListener(recharge);
        heal.onClick.AddListener(Heal);
    }

    //The update function is used to update the coins text
    public void Update()
    {
        coins.text = "Coins: " + playerUnit.stats.coins;
    }

    //The heal function is used to upgrade attack
    public void Att()
    {
        Attack.text= "ATT: " + playerUnit.stats.damage;
        if (playerUnit.stats.coins >= 10)
        {
            playerUnit.stats.coins -= 10;
            playerUnit.stats.damage += 3;
            dialogueText.text = "You bought 3 attack!";
            Attack.text= "ATT: " + playerUnit.stats.damage;
        }
        else
        {
            dialogueText.text = "You don't have enough coins!";
        }
    }

    //ThIS function is used to upgrade Defence
    public void Def()
    {
        Defence.text= "DEF: " + playerUnit.stats.defence;
        if (playerUnit.stats.coins >= 10)
        {
            playerUnit.stats.coins -= 10;
            playerUnit.stats.defence += 3;
            dialogueText.text = "You bought 3 defence!";
            Defence.text= "DEF: " + playerUnit.stats.defence;

        }
        else
        {
            dialogueText.text = "You don't have enough coins!";
        }
    }

    //This function is used to upgrade agility
    public void Agl()
    {
        Agility.text= "AGL: " + playerUnit.stats.agility;
        if (playerUnit.stats.coins >= 10)
        {
            playerUnit.stats.coins -= 10;
            playerUnit.stats.agility += 3;
            dialogueText.text = "You bought 3 agility!";
            Agility.text= "AGL: " + playerUnit.stats.agility;
            
        }
        else
        {
            dialogueText.text = "You don't have enough coins!";
        }
    }

    //This function is used to upgrade luck
    public void Lck()
    {
        Luck.text= "LCK: " + playerUnit.stats.luck;
        if (playerUnit.stats.coins >= 10)
        {
            playerUnit.stats.coins -= 10;
            playerUnit.stats.luck += 3;
            dialogueText.text = "You bought 3 luck!";
            Luck.text= "LCK: " + playerUnit.stats.luck;
        }
        else
        {
            dialogueText.text = "You don't have enough coins!";
        }
    }

    //This function is used to upgrade accuracy
    public void Acc()
    {
        Accuracy.text= "ACC: " + playerUnit.stats.accuracy;
        if (playerUnit.stats.coins >= 10)
        {
            playerUnit.stats.coins -= 10;
            playerUnit.stats.accuracy += 3;
            dialogueText.text = "You bought 3 accuracy!";
            Accuracy.text= "ACC: " + playerUnit.stats.accuracy;
        }
        else
        {
            dialogueText.text = "You don't have enough coins!";
        }
    }

    //This function is used to upgrade charisma
    public void Chr()
    {
        Charisma.text= "CHR: " + playerUnit.stats.charisma;
        if (playerUnit.stats.coins >= 10)
        {
            playerUnit.stats.coins -= 10;
            playerUnit.stats.charisma += 3;
            dialogueText.text = "You bought 3 charisma!";
            Charisma.text= "CHR: " + playerUnit.stats.charisma;
        }
        else
        {
            dialogueText.text = "You don't have enough coins!";
        }
    }

    //This function is used to upgrade the MAXHP of the player
    public void hp()
    {
        HPText.text= "MAXHP: " + playerUnit.stats.maxHP;
        if (playerUnit.stats.coins >= 15)
        {
            playerUnit.stats.coins -= 15;
            playerUnit.stats.maxHP += 5;
            dialogueText.text = "You bought 5 HP!";
            HPText.text= "MAXHP: " + playerUnit.stats.maxHP;
        }
        else
        {
            dialogueText.text = "You don't have enough coins!";
        }
    }

    //This function is used to upgrade the MAXMP of the player
    public void mp()
    {
        MPText.text= "MAXMP: " + playerUnit.stats.maxMP;
        if (playerUnit.stats.coins >= 15)
        {
            playerUnit.stats.coins -= 15;
            playerUnit.stats.maxMP += 5;
            dialogueText.text = "You bought 5 MP!";
            MPText.text= "MAXMP: " + playerUnit.stats.maxMP;
        }
        else
        {
            dialogueText.text = "You don't have enough coins!";
        }

    }

    //This function is used to recharge the MP of the player
    public void recharge()
    {
        if (playerUnit.stats.currentMP == playerUnit.stats.maxMP)
        {
            dialogueText.text = "You are already at full MP!";
        }
        else{
            if (playerUnit.stats.coins >= 3)
            {
                playerUnit.stats.coins -= 3;
                playerUnit.stats.currentMP = playerUnit.stats.maxMP;
                dialogueText.text = "You recharged!";
            }
            else
            {
                dialogueText.text = "You don't have enough coins!";
            }
        }
    }

    //This function is used to heal the player
    public void Heal()
    {
        if (playerUnit.stats.currentHP == playerUnit.stats.maxHP)
        {
            dialogueText.text = "You are already at full health!";
        }
        else{
            if (playerUnit.stats.coins >= 3)
            {
                playerUnit.stats.coins -= 3;
                playerUnit.stats.currentHP = playerUnit.stats.maxHP;
                
                dialogueText.text = "You healed!";
            }
            else
            {
                dialogueText.text = "You don't have enough coins!";
            }
        }
    }
}