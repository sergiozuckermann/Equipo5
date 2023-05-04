//Code by Zaza Team
// Description: This code is used to store the stats of the player and the enemy. It also contains the functions that are used to change the stats of the player and the enemy.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using UnityEngine.SceneManagement;


using System;

//This class is used to store the stats of the player and the enemy
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

//This class is used to store the game session data of the player

[System.Serializable]
public class GameSessionData
{
    public int game_session_id;
    public int player_id;
}


public class unit : MonoBehaviour
{
	
    public Stats stats;
	public string unitName;

	//This function is used to set the hp of the player and the enemy after taking damage
    public bool TakeDamage(int dmg)
	{	
		stats.currentHP -= dmg;

		if (stats.currentHP <= 0)
			return true;
		else
			return false;
	}

	//This function is used to set the HP of the player after healing
	public void Heal(int amount)
	{
		stats.currentHP += amount;
		if (stats.currentHP > stats.maxHP)
			stats.currentHP = stats.maxHP;
	}

	//This function is used to set the MP of the player after recharging
	public void Recharge(int amount)
	{
		stats.currentMP += amount;
		if (stats.currentMP > stats.maxMP)
			stats.currentMP = stats.maxMP;
	}

	//This function is used to set the freezing effects (Total turns frozen) of the player or enemy after using the ice element
	public void setice(int num)
	{
		stats.ice = num;
	}

	//This function is used to decrease the freezing effects (Total turns frozen) of the player or enemy after a turn has passed	
	public void decreaseice()
	{
		stats.ice -= 1;
	}

	//This function is used to decrease the burning effects (Total turns burned) of the player or enemy after a turn has passed
	public void decreasefire()
	{
		stats.fire -= 1;
	}

	//This function is used to set the burning effects (Total turns burned) of the player or enemy after using the fire element
	public void setfire()
	{
		stats.fire = 3;
	}

	//This function is used to set the lightning effects (Total turns buffed) of the player or enemy after using the lightning element
	public void setlightning(int num)
	{
		stats.lightning = num;
	}

	//This function is used to set the MP after using a move that consumes MP
	public void setmp(int num)
	{
		stats.currentMP = num;
	}

	//This function is used to duplicate the strenght stats of the player or enemy after using the lightning element
	public void lightningbuff(){
	stats.damage=stats.damage*2;
	}

	//This function is used to decrease the lightning buffed stats of the player or enemy after a turn has passed
	public void lightningnerf(){
	stats.damage=stats.damage/2;
	}
	
	//The next 3 functions are used to set the stats of the class that the player has chosen
	//It initializes the characters unit, position, scene and state of the enemies ("Dead")  in player prefs
	//It also initializes the Combat counters to store in player prefs
	public void Lightclass(){

	stats.maxHP= 30;
    stats.currentHP= 25;
	stats.maxMP= 60;
    stats.currentMP= 60;
	stats.damage=5;
    stats.luck= 5;
    stats.agility= 5;
    stats.defence= 3;
	stats.charisma= 2;
	stats.accuracy= 6;
	stats.coins=10;
	stats.firea=false;
	stats.icea=false;
	stats.lightninga=false;
	PlayerPrefs.SetInt("place", 1);
	PlayerPrefs.SetInt("Dead", 0);
	PlayerPrefs.SetFloat("x", Convert.ToSingle(-10.4));
    PlayerPrefs.SetFloat("y", Convert.ToSingle(1.5));
    string savedShaggy=JsonUtility.ToJson(stats);

    PlayerPrefs.SetString("Shaggy", savedShaggy);
    PlayerPrefs.SetInt("Class_id", 1);

  	PlayerPrefs.SetString("Damagemade", "");
	PlayerPrefs.SetString("Damagereceived", "");
	PlayerPrefs.SetString("Coinsmade", "");
	PlayerPrefs.SetString("Misses", "");
	PlayerPrefs.SetString("Crits", "");
	PlayerPrefs.SetString("Ice", "");
	PlayerPrefs.SetString("Fire", "");
	PlayerPrefs.SetString("Lightning", "");
	PlayerPrefs.SetString("Melee", "");
	PlayerPrefs.SetString("Heal", "");
	PlayerPrefs.SetString("Recharge", "");
	PlayerPrefs.SetString("Result", "");
    PlayerPrefs.SetString("Enemy", "");

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
	PlayerPrefs.SetInt("place", 1);
	PlayerPrefs.SetInt("Dead", 0);
	PlayerPrefs.SetFloat("x", Convert.ToSingle(-10.4));
    PlayerPrefs.SetFloat("y", Convert.ToSingle(1.5));
	string savedShaggy=JsonUtility.ToJson(stats);
    PlayerPrefs.SetString("Shaggy", savedShaggy);
	PlayerPrefs.SetInt("Class_id", 2);
	PlayerPrefs.SetInt("Damagemade", 0);
	PlayerPrefs.SetInt("Damagereceived", 0);
	PlayerPrefs.SetInt("Coinsmade", 0);
	PlayerPrefs.SetInt("Misses", 0);
	PlayerPrefs.SetInt("Crits", 0);
	PlayerPrefs.SetInt("Ice", 0);
	PlayerPrefs.SetInt("Fire", 0);
	PlayerPrefs.SetInt("Lightning", 0);
	PlayerPrefs.SetInt("Melee", 0);

	
    PlayerPrefs.SetString("Shaggy", savedShaggy);
    PlayerPrefs.SetInt("Class_id", 1);

  	PlayerPrefs.SetString("Damagemade", "");
	PlayerPrefs.SetString("Damagereceived", "");
	PlayerPrefs.SetString("Coinsmade", "");
	PlayerPrefs.SetString("Misses", "");
	PlayerPrefs.SetString("Crits", "");
	PlayerPrefs.SetString("Ice", "");
	PlayerPrefs.SetString("Fire", "");
	PlayerPrefs.SetString("Lightning", "");
	PlayerPrefs.SetString("Melee", "");
	PlayerPrefs.SetString("Heal", "");
	PlayerPrefs.SetString("Recharge", "");
	PlayerPrefs.SetString("Result", "");
	PlayerPrefs.SetString("Enemy", "");

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
	PlayerPrefs.SetInt("place", 1);
	PlayerPrefs.SetInt("Dead", 0);
	PlayerPrefs.SetFloat("x", Convert.ToSingle(-10.4));
    PlayerPrefs.SetFloat("y", Convert.ToSingle(1.5));
	string savedShaggy=JsonUtility.ToJson(stats);
    PlayerPrefs.SetString("Shaggy", savedShaggy);
	PlayerPrefs.SetInt("Class_id", 3);


  	PlayerPrefs.SetString("Damagemade", "");
	PlayerPrefs.SetString("Damagereceived", "");
	PlayerPrefs.SetString("Coinsmade", "");
	PlayerPrefs.SetString("Misses", "");
	PlayerPrefs.SetString("Crits", "");
	PlayerPrefs.SetString("Ice", "");
	PlayerPrefs.SetString("Fire", "");
	PlayerPrefs.SetString("Lightning", "");
	PlayerPrefs.SetString("Melee", "");
	PlayerPrefs.SetString("Heal", "");
	PlayerPrefs.SetString("Recharge", "");
	PlayerPrefs.SetString("Result", "");
	PlayerPrefs.SetString("Enemy", "");

	SaveData();
	}

	//This is the code for when the upgrade buttons are called, it increases the stats by 3 and decrease the coins by 10 or 15 depending on the upgrade
	public void onDefencebutton(){
		stats.defence=stats.defence+3;
		
		stats.coins=stats.coins -10;
	}

	public void onAttackbutton(){
		stats.damage=stats.damage+3;
		stats.coins=stats.coins -10;
	}

	public void onLuckbutton(){
		stats.luck=stats.luck+3;
		stats.coins=stats.coins -10;
	}

	public void onAgilitybutton(){
		stats.agility=stats.agility+3;
		stats.coins=stats.coins -10;
	}

	public void onCharismabutton(){
		stats.charisma=stats.charisma+3;
		stats.coins=stats.coins -10;
	}

	public void onAccuracybutton(){
		stats.accuracy=stats.accuracy+3;
		stats.coins=stats.coins -10;
	}

	public void onTotalHPbutton(){
		stats.maxHP=stats.maxHP+5;
		stats.coins=stats.coins -15;
	}

	public void onTotalMPbutton(){
		stats.maxMP=stats.maxMP+5;
		stats.coins=stats.coins -15;
	}

	public void onMAXHPbutton(){
		stats.currentHP=stats.maxHP;
		stats.coins=stats.coins -10;
	}

	public void onMAXMPbutton(){
		stats.currentMP=stats.maxMP;
		stats.coins=stats.coins -10;
	}

	//This is the code for sending a request to the server to save the game
 	IEnumerator SendRequest(NewGame newgame, string url, string route)
    {
        string json = JsonUtility.ToJson(newgame);

        using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:3010/api/new_game_session", json))
        {
            
            Debug.Log("Sending request");
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.responseCode == 200)
            {
                Debug.Log("Game saved");
				GameSessionData data = JsonUtility.FromJson<GameSessionData>(www.downloadHandler.text);
				PlayerPrefs.SetInt("Game_session_id", data.game_session_id);
				PlayerPrefs.SetInt("Player_id", data.player_id);
				PlayerPrefs.SetInt("is_finished", 0);
				Debug.Log("Game session id: " + data.game_session_id);
				Debug.Log("Player id: " + data.player_id);
            }
             else
            {
                Debug.Log("failed!");
            }
        }
    }

	//This is the code for saving the game by getting the unit stats, user id and class id and sending them to the server
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

