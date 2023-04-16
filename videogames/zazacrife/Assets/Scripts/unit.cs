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
	
	


}
