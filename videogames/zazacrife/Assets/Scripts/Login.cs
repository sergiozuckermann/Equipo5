// Code by Zaza Team
// Description: This script is used to login the user and save the username and password to a file or database.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

//This class is used to save the username and password to a file or database
 [System.Serializable]
 public class User
 {
     public string username;
     public string password;
 }

 public class Login : MonoBehaviour, IDataPersistance
 {
     public TMP_InputField username;
     public TMP_InputField password;

    //This function is used to send the request of the login data to the API.
     IEnumerator SendRequest(User user, string url, string route)
     {
        Debug.Log("Sending request");
        Debug.Log(username.text);


         string json = JsonUtility.ToJson(user);
          //Here you can add your code to send the username and password to a server
         using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:3010/api/login", json))
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
                 PlayerPrefs.SetInt("User_id",int.Parse(www.downloadHandler.text));
                 int user_id = PlayerPrefs.GetInt("User_id");
                 GetComponent<Login>().PlayGame();
             }
             else
             {
                 Debug.Log("Login failed!");
             }
         }
     }   
     
    //This function is used to load the game scene.

    public void LoadData(GameData data)
    {
         this.username.text = data.username;
    }

    //This function is used to save the game data.
    public void SaveData(ref GameData data)
    {
         data.username = this.username.text;     
    }

    //This function is used to log the username and password.
     public void LogInput()
    {
        User user = new User();
         user.username = username.text;
         user.password  = password.text;

         gameObject.SetActive(true);
         StartCoroutine(SendRequest(user, "http:localhost:3010", "api/login"));


        //Here you can add your code to save the username and password to a file or database
    }
    
    //This function is used to load the game scene.
     public void PlayGame(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     }

 }
