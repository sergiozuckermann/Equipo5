using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI HPText;
	//public Slider hpSlider;

	public void SetHUD(unit unit)
	{
		HPText.text = unit.currentHP.ToString();
		nameText.text = unit.unitName;
		
		// hpSlider.maxValue = unit.maxHP;
		// hpSlider.value = unit.currentHP;
	}

	public void SetHP(int hp)
	 {
	 	HPText.text = hp.ToString();
	 }

}

