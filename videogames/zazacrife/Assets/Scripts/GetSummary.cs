//Done by Zaza Team
// Description: This script is used to get the summary of the game session of the user.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

//This class is used to store the summary data.
public class SummaryData
{
    public int user_id;
    public string player_name;
    public string class_name;
    public int money;
}

//This class is used to store the summary data wrapper.
[System.Serializable]
public class SummaryDataWrapper
{
    public SummaryData[] data;
}

public class GetSummary : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI clas;
    public TextMeshProUGUI money;
    public SummaryData data;

    //This function is used to send the request of the summary data to the API.
    IEnumerator SendRequest(string url)
    {

        Debug.Log("url");
        Debug.Log(url);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Success: " + www.downloadHandler.text);
                string jsonResponse = www.downloadHandler.text;
                data = JsonUtility.FromJson<SummaryData>(www.downloadHandler.text);
                
                name.text = data.player_name;
                clas.text = data.class_name;
                money.text = data.money.ToString();
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        }
    }

    //This function is used to get the summary data from the database.
    public void OnMouseDown()
    {
    string user_id = PlayerPrefs.GetString("User_id");
    string url = "http://localhost:3000/api/game_sessions?user_id=" + user_id;
    StartCoroutine(SendRequest(url));

    }

}