//Code by Zaza Team
// Description: This script is used to call unit methods at the class selection menu part of the game to iniatialize the character.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Inicio : MonoBehaviour
{

    public GameObject Shaggy; // Declare a public GameObject variable called playerPrefab
    unit playerUnit; // Declare a public unit variable called playerUnit

    

    // Start will initialize the playerUnit variable with the unit component of the playerPrefab.
    void Start()
    {
        
        playerUnit = Shaggy.GetComponent<unit>();

    }

 
    
    //These functions are used to call the unit methods to initialize the character.
    public void OnLightClassButton()
    {      
        playerUnit.Lightclass();      
    }

    public void OnMiddleClassButton()
    {      
        playerUnit.Middleclass();      
    }

    public void OnHeavyClassButton()
    {      
        playerUnit.Heavyclass();      
    }
    
}
