using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit : MonoBehaviour
{
    public string unitName;
   

    public int damage;
    public int maxHP;
    public int currentHP;
	public int maxMP;
    public int currentMP;
    public int luck;
    public int agility;
    public int defence;
	public int charisma;
	public int accuracy;

	public int fire = 0;
	public int ice = 0;
	public int lightning = 0;

	
	

    public bool TakeDamage(int dmg)
	{	
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

	public void Recharge(int amount)
	{
		currentMP += amount;
		if (currentMP > maxMP)
			currentMP = maxMP;
	}

	public void setice(int num)
	{
		ice = num;
	}

		public void decreaseice()
	{
		ice -= 1;
	}

	public void decreasefire()
	{
		fire -= 1;
	}

	public void setfire()
	{
		fire = 3;
	}


	public void setlightning(int num)
	{
		lightning = num;
	}

	public void setmp(int num)
	{
		currentMP = num;
	}

	public void lightningbuff(){
	damage=damage*2;
    luck= luck*2;
    agility= agility*2;
    defence= defence*2;
	charisma= charisma*2;
	accuracy= accuracy*2;
	}


	public void lightningnerf(){
	damage=damage/2;
    luck= luck/2;
    agility= agility/2;
    defence= defence/2;
	charisma= charisma/2;
	accuracy= accuracy/2;
	}
	
	public void Lightclass(){
	maxHP= 30;
    currentHP= 25;
	maxMP= 60;
    currentMP= 60;
	damage=5;
    luck= 5;
    agility= 5;
    defence= 3;
	charisma= 2;
	accuracy= 6;
	}

	public void Middleclass(){
	maxHP= 40;
    currentHP= 37;
	maxMP= 50;
    currentMP= 50;
	damage=8;
	luck= 3;
	agility= 3;
	defence= 5;
	charisma= 2;
	accuracy= 4;
	}

	public void Heavyclass(){
	maxHP= 50;
    currentHP= 50;
	maxMP= 30;
    currentMP= 30;
	damage=10;
	luck= 2;
	agility= 1;
	defence= 7;
	charisma= 2;
	accuracy= 2;
	}

	


}
