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
    

    
    public void OnDisplayButton()
    {
    unit playerUnit = Shaggy.GetComponent<unit>();
    DEFText.text =  playerUnit.stats.defence.ToString();
    ACCText.text =  playerUnit.stats.accuracy.ToString();
    LUCKText.text =  playerUnit.stats.luck.ToString();
    AGIText.text =  playerUnit.stats.agility.ToString();
    CHAText.text =  playerUnit.stats.charisma.ToString();
    ATTText.text =  playerUnit.stats.damage.ToString();
    HPText.text =   playerUnit.stats.currentHP.ToString();
    MPText.text =   playerUnit.stats.currentMP.ToString();
    }

  
}
