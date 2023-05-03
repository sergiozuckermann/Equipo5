using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame
{
    public string stats;
    public int game_session_id;
    public int player_id;
    public float x;
    public float y;
    public int is_finished;
}

public class LoadSession : MonoBehaviour
{

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
                Debug.Log(data.game_session_id);
            
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        }
    }

    public void LoadGameData()
    {

        StartCoroutine(SendRequest("http://localhost:3010/api/get_game_session?user_id=" + PlayerPrefs.GetInt("User_id").ToString()));
    }
    
}
