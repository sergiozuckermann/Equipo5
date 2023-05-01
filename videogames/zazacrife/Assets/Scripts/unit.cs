using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


[System.Serializable]
public class Stats{

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
	public int dead;
	public int fire = 0;
	public int ice = 0;
	public int lightning = 0;
	public int coins = 0;
	public int index = 0;
	public int place;
	public int number;
	public bool firea;
	public bool icea;
	public bool lightninga;

	

}


public class unit : MonoBehaviour
{

	

    public Stats stats;
	public string unitName;
	
	

	



    public bool TakeDamage(int dmg)
	{	
		stats.currentHP -= dmg;

		if (stats.currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal(int amount)
	{
		stats.currentHP += amount;
		if (stats.currentHP > stats.maxHP)
			stats.currentHP = stats.maxHP;
	}

	public void Recharge(int amount)
	{
		stats.currentMP += amount;
		if (stats.currentMP > stats.maxMP)
			stats.currentMP = stats.maxMP;
	}

	public void setice(int num)
	{
		stats.ice = num;
	}

		public void decreaseice()
	{
		stats.ice -= 1;
	}

	public void decreasefire()
	{
		stats.fire -= 1;
	}

	public void setfire()
	{
		stats.fire = 3;
	}


	public void setlightning(int num)
	{
		stats.lightning = num;
	}

	public void setmp(int num)
	{
		stats.currentMP = num;
	}

	public void lightningbuff(){
	stats.damage=stats.damage*2;
	}


	public void lightningnerf(){
	stats.damage=stats.damage/2;
	}
	
	public void Lightclass(){
    stats.maxHP = 30;
    stats.currentHP = 25;
    stats.maxMP = 60;
    stats.currentMP = 60;
    stats.damage = 5;
    stats.luck = 5;
    stats.agility = 5;
    stats.defence = 3;
    stats.charisma = 2;
    stats.accuracy = 6;
    stats.coins = 10;
    stats.firea = false;
    stats.icea = false;
    stats.lightninga = false;
    string savedShaggy = JsonUtility.ToJson(stats);
    PlayerPrefs.SetString("Shaggy", savedShaggy);
    PlayerPrefs.SetInt("Class_id", 1);
	SaveData();
}

	public void Middleclass(){
	stats.maxHP= 40;
    stats.currentHP= 37;
	stats.maxMP= 50;
    stats.currentMP= 50;
	stats.damage=8;
	stats.luck= 3;
	stats.agility= 3;
	stats.defence= 5;
	stats.charisma= 2;
	stats.accuracy= 4;
	stats.coins=10;
	stats.firea=false;
	stats.icea=false;
	stats.lightninga=false;
	string savedShaggy=JsonUtility.ToJson(stats);
    PlayerPrefs.SetString("Shaggy", savedShaggy);
	PlayerPrefs.SetInt("Class_id", 2);
	SaveData();
	}

	public void Heavyclass(){
	stats.maxHP= 50;
    stats.currentHP= 50;
	stats.maxMP= 30;
    stats.currentMP= 30;
	stats.damage=10;
	stats.luck= 2;
	stats.agility= 1;
	stats.defence= 7;
	stats.charisma= 2;
	stats.accuracy= 2;
	stats.coins=10;
	stats.firea=false;
	stats.icea=false;
	stats.lightninga=false;
	string savedShaggy=JsonUtility.ToJson(stats);
    PlayerPrefs.SetString("Shaggy", savedShaggy);
	PlayerPrefs.SetInt("Class_id", 3);
	SaveData();
	}

	public void onDefencebutton(){
		stats.defence=stats.defence+1;
	}

	public void onAttackbutton(){
		stats.damage=stats.damage+1;
	}

	public void onLuckbutton(){
		stats.luck=stats.luck+1;
	}

	public void onAgilitybutton(){
		stats.agility=stats.agility+1;
	}

	public void onCharismabutton(){
		stats.charisma=stats.charisma+1;
	}

	public void onAccuracybutton(){
		stats.accuracy=stats.accuracy+1;
	}

	public void onTotalHPbutton(){
		stats.maxHP=stats.maxHP+5;
	}

	public void onTotalMPbutton(){
		stats.maxMP=stats.maxMP+5;
	}


 IEnumerator SendRequest(NewGame newgame, string url, string route)
    {
        string json = JsonUtility.ToJson(newgame);
          //Here you can add your code to send the username and password to a server
        using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:3010/api/new_game_session", json))
        {
            
            Debug.Log("Sending request");
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();
            Debug.Log("Request sent");
            Debug.Log(www.downloadHandler.text);
            Debug.Log(www.error);
            Debug.Log(www.responseCode);
            Debug.Log("result");
            Debug.Log(www.result);

            if (www.responseCode == 200)
            {
                Debug.Log("Game saved");

            }
             else
            {
                Debug.Log("failed!");
            }
        }
    }
    public void SaveData()
    {
        Debug.Log("Saving game");
        NewGame newgame = new NewGame();
        newgame.stats = PlayerPrefs.GetString("Shaggy");
        newgame.user_id = PlayerPrefs.GetInt("User_id");
        newgame.class_id = PlayerPrefs.GetInt("Class_id");
        StartCoroutine(SendRequest(newgame, "http://localhost:3010/api/new_game_session", "new_game_session"));
    }
}

