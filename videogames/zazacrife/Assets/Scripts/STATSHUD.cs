//Done by Zaza team
// Description: This script is used to display the stats of the player in the stats HUD.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class STATSHUD : MonoBehaviour
{
    public GameObject Shaggy;
    public TextMeshProUGUI DEFText;
    public TextMeshProUGUI ACCText;
    public TextMeshProUGUI LUCKText;
    public TextMeshProUGUI AGIText;
    public TextMeshProUGUI CHAText;
    public TextMeshProUGUI ATTText;
	public TextMeshProUGUI HPText;
	public TextMeshProUGUI MPText;
    public TextMeshProUGUI COINSTexts;

    //This function is used to display the stats of the player in the stats HUD.
    public void OnDisplayButton()
    {
        unit playerUnit = Shaggy.GetComponent<unit>();
        DEFText.text = playerUnit.stats.defence.ToString();
        ACCText.text = playerUnit.stats.accuracy.ToString();
        LUCKText.text = playerUnit.stats.luck.ToString();
        AGIText.text = playerUnit.stats.agility.ToString();
        CHAText.text = playerUnit.stats.charisma.ToString();
        ATTText.text = playerUnit.stats.damage.ToString();
        HPText.text = playerUnit.stats.maxHP.ToString();
        MPText.text = playerUnit.stats.maxMP.ToString();
        COINSTexts.text= playerUnit.stats.coins.ToString();
    }
}
