using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

[System.Serializable]
public class User
{
    public string username;
    public string password;
}


public class Regiseter : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;


    IEnumerator SendRequest(User user, string url, string route)
    {

        string json = JsonUtility.ToJson(user);

        // Here you can add your code to send the username and password to a server
        using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:3000/api/new_user", json))
        {
            
            Debug.Log("Sending request");
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            Debug.Log(www.downloadHandler.text);
            Debug.Log(www.error);
            Debug.Log(www.responseCode);
            Debug.Log(www.result);
        }
    }   

    public void LogInput()
    {
        User user = new User();
        user.username = username.text;
        user.password  = password.text;

        gameObject.SetActive(true);
        StartCoroutine(SendRequest(user, "http://localhost:3000", "/api/new_user"));


        // Here you can add your code to save the username and password to a file or database
    }
    


}
