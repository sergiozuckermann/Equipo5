//Made by Zaza Team
// Description: Class that contains the data that will be saved in the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int deathCount;
    public string username;
    public Vector3 playerPosition;
    // public SerialDiccionary<string, bool> elementCollected;

    //Constructor
    public GameData(){
        this.deathCount = 0;
        this.username = "";
        playerPosition = Vector3.zero;
        // elementCollected = new SerialDiccionary<string, bool>();
    }
}
