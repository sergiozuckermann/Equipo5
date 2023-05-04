//Code by Zaza Team
// Description: This script is used to load the game session data from the database.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

//This class is used to store the game session data.
public class LoadGame
{
    public string shaggy;
    public int game_session_id;
    public int player_id;
    public float x;
    public float y;
    public int is_finished;
    public int place;
    public int class_id;
}

public class LoadSession : MonoBehaviour
{
    public LoadGame data;
    
    //This function is used to send the request of the game session data to the API.
    IEnumerator SendRequest(string url)
    {
        Debug.Log(url);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Success: " + www.downloadHandler.text);
                string jsonResponse = www.downloadHandler.text;
                data = JsonUtility.FromJson<LoadGame>(www.downloadHandler.text);
                LoadGameScene();
                SetPlayersAttributes();
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        }
    }

    //This function is used to load the game session data.
    public void LoadGameData()
    {

        StartCoroutine(SendRequest("http://localhost:3010/api/get_game_session?user_id=" + PlayerPrefs.GetInt("User_id").ToString()));
    }

    public void LoadGameScene()
    {
        if (data.place == 1)
            {
            SceneManager.LoadScene("Tilemap");
            }
        else
            {
            SceneManager.LoadScene("Torre");
            }
    }

    //This function is used to set and clean the players attributes.
    public void SetPlayersAttributes()
    {
        PlayerPrefs.SetFloat("x", Convert.ToSingle(data.x));
        PlayerPrefs.SetFloat("y", Convert.ToSingle(data.y));
        PlayerPrefs.SetInt("Dead", 0);
        PlayerPrefs.SetInt("place", data.place);


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
        PlayerPrefs.SetInt("Game_session_id", data.game_session_id);
		PlayerPrefs.SetInt("Player_id", data.player_id);
        PlayerPrefs.SetInt("is_finished", data.is_finished);
        Debug.Log("data.shaggy: " + data.shaggy);
        PlayerPrefs.SetString("Shaggy", data.shaggy);
        PlayerPrefs.SetInt("Class_id", data.class_id);
    }


    
}
