//Code by Zaza Team
// Description: This script is used to initialize the Log In part of the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputBoxLogger : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;

    //This function is used to log the username and password.
    public void LogInput()
    {
        string usernameM = username.text;
        string passwordM = password.text;
        Debug.Log("Username: " + usernameM);
        Debug.Log("Password: " + passwordM);

        // Here you can add your code to save the username and password to a file or database
    }
    
}