//CODE BY ZAZA TEAM
//THIS SCRIPT IS FOR UPDATING THE BATTLE HUD OF THE GAME (HP, MP, NAME, ETC)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI HPText;
	public TextMeshProUGUI MPText;

	//This function is used to set the HUD of the player and the enemy in the start of the battle scene

	public void SetHUD(unit unit)
	{
		HPText.text = "HP: " + unit.stats.currentHP.ToString();
		nameText.text = unit.unitName;
	}


	//This function is used to update the MP of the player and the enemy in the battle scene
	public void SetMP(int mp)
	{
		MPText.text = "MP: " + mp.ToString();
	}

	//This function is used to update the HP of the player and the enemy in the battle scene
	public void SetHP(int hp)
	 {
	 	HPText.text = "HP: " + hp.ToString();
	 }

}

