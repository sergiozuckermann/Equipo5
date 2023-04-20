using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Inicio : MonoBehaviour
{

    public GameObject Shaggy; // Declare a public GameObject variable called playerPrefab
    unit playerUnit; // Declare a public unit variable called playerUnit

    

    // Start is called before the first frame update
    void Start()
    {
        playerUnit = Shaggy.GetComponent<unit>();

    }

 
    

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
