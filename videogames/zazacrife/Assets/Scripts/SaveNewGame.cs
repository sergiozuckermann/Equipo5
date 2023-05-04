// Code by Zaza team
// Description: This script is used to save the new game data to the database.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NewGame{
    public string stats;
    public int user_id;
    public int class_id;
}

public class SaveNewGame : MonoBehaviour
{
    //This function is used to send the request of the new game data to the API.
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

    //This function is used to save the new game data to the database.      
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
