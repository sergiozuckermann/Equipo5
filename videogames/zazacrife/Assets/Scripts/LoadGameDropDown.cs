//Code by Zaza Team
// Description: This script is used to iniatialize the LoadGameDropDown.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;


//This class is used to store the game session ids and player ids.
public class GameSessionIds
{
    public int[] game_session_id ;
    public int[] player_id;
}


public class LoadGameDropDown : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Dropdown myDropdown;
    public GameSessionIds wrapper;

    //This function is used to load the game data.
    public void Start()
    {
        GameObject dropdownGameObject = GameObject.FindGameObjectWithTag("dropDown");

        myDropdown = dropdownGameObject.GetComponent<TMP_Dropdown>();

        // Create a new list of Dropdown options
        List<string> options = new List<string>();
        options.Add("Option 1");
        options.Add("Option 2");
        options.Add("Option 3");

        // Clear existing options and add the new options to the Dropdown
        myDropdown.ClearOptions();
        myDropdown.AddOptions(options);
        LoadGameData();
        // Add listener for when the value of the Dropdown changes, to take action
        

        // Set the initial value of the dropdown
        myDropdown.value = 0;
    }


    //This function is used to send the request to the server.
    IEnumerator SendRequest(string url)
    {


        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Success: " + www.downloadHandler.text);
                string jsonResponse = www.downloadHandler.text;

                Debug.Log(jsonResponse);
                wrapper = JsonUtility.FromJson<GameSessionIds>(jsonResponse);
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        }
    }

    //This function is used to load the game data.
    public void LoadGameData()
    {
        int user_id = PlayerPrefs.GetInt("User_id");
        Debug.Log(user_id);
        string url = "http://localhost:3010/api/get_game_session_id?user_id=" + user_id.ToString();
        StartCoroutine(SendRequest(url));
    }
}


