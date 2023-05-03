using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;




public class Game{
    public string stats;
    public int game_session_id;
    public int player_id;
    public float x;
    public float y;
    public int is_finished;
    public int place;
    //BATTLE STATS
    public string damagemades;
    public string damagereceiveds;
    public string coinsreceiveds;
    public string missess;
    public string critss;
    public string melees;
    public string iceusess;
    public string fireusess;
    public string thunderusess;
    public string healusess;
    public string rechargeusess;
    public string results;
}

public class SaveGame : MonoBehaviour
{
    [SerializeField] private GameObject Shaggy;

    
    IEnumerator SendRequest(Game game)
    {
        

        string json = JsonUtility.ToJson(game);
          //Here you can add your code to send the username and password to a server
        using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:3010/api/update_game_session", json))
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

        


        Debug.Log("Saving data");
        Game game = new Game();
        game.stats = PlayerPrefs.GetString("Shaggy");
        game.player_id = PlayerPrefs.GetInt("Player_id");
        game.game_session_id = PlayerPrefs.GetInt("Game_session_id");
        game.x = Shaggy.transform.position.x;
        game.y = Shaggy.transform.position.y;
        game.place = PlayerPrefs.GetInt("place");
        
        game.damagemades=PlayerPrefs.GetString("Damagemade");
        game.damagereceiveds=PlayerPrefs.GetString("Damagereceived");
        game.coinsreceiveds=PlayerPrefs.GetString("Coinsmade");
        game.missess=PlayerPrefs.GetString("Misses");
        game.critss=PlayerPrefs.GetString("Crits");
        game.melees=PlayerPrefs.GetString("Melee");
        game.iceusess=PlayerPrefs.GetString("Ice");
        game.fireusess=PlayerPrefs.GetString("Fire");
        game.thunderusess=PlayerPrefs.GetString("Lightning");
        game.healusess=PlayerPrefs.GetString("Heal");
        game.rechargeusess=PlayerPrefs.GetString("Recharge");
        game.results=PlayerPrefs.GetString("Result");

        
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

        game.is_finished = PlayerPrefs.GetInt("is_finished");
        Debug.Log("Sending Data");
        StartCoroutine(SendRequest(game));
    }
    /*
    IEnumerator LoadGameData() {
        // Send the GET request to the PHP script to retrieve the saved game data
        using (UnityWebRequest www = UnityWebRequest.Get(loadDataUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Game data loaded successfully!");
                string jsonResponse = www.downloadHandler.text;
                // Parse the retrieved JSON string into game data variables
                JSONObject jsonObject = new JSONObject(jsonResponse);
                playerName = jsonObject.GetField("playerName").str;
                float posX = float.Parse(jsonObject.GetField("playerPosX").str);
                float posY = float.Parse(jsonObject.GetField("playerPosY").str);
                playerPosition = new Vector2(posX, posY);
                playerCoins = (int)jsonObject.GetField("playerCoins").n;
                playerLevel = (int)jsonObject.GetField("playerLevel").n;

                // Update the player object with the loaded game data
                gameObject.name = playerName;
                gameObject.transform.position = new Vector3(playerPosition.x, playerPosition.y, 0);
                // Update other player stats as necessary
                // ...
            }
            else
            {
                Debug.Log("Error loading game data: " + www.error);
            }
        }
    }*/
}

