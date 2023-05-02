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
        game.is_finished = PlayerPrefs.GetInt("is_finished");
        Debug.Log("Sending Data");
        StartCoroutine(SendRequest(game));
    }
}
