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

	//public Slider hpSlider;

	public void SetHUD(unit unit)
	{
		HPText.text = "HP: " + unit.stats.currentHP.ToString();
		nameText.text = unit.unitName;
		
		// hpSlider.maxValue = unit.maxHP;
		// hpSlider.value = unit.currentHP;
	}



	public void SetMP(int mp)
	{
		MPText.text = "MP: " + mp.ToString();
	}

	public void SetHP(int hp)
	 {
	 	HPText.text = "HP: " + hp.ToString();
	 }

}

