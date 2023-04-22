using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[System.Serializable]
public class User
{
    public string username;
    public string password;
}


public class Login : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;


    IEnumerator SendRequest(User user, string url, string route)
    {

        string json = JsonUtility.ToJson(user);

        // Here you can add your code to send the username and password to a server
        using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:3000/api/login", json))
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
                PlayerPrefs.SetString("User_id", www.downloadHandler.text);
                string user_id = PlayerPrefs.GetString("User_id");
                GetComponent<Login>().PlayGame();

            }
            else
            {
                Debug.Log("Login failed!");
            }
        }
    }   

    public void LogInput()
    {
        User user = new User();
        user.username = username.text;
        user.password  = password.text;

        gameObject.SetActive(true);
        StartCoroutine(SendRequest(user, "http://localhost:3000", "api/login"));


        // Here you can add your code to save the username and password to a file or database
    }
    
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
