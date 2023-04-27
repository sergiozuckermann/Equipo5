using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int deathCount;
    public string userName;
    public GameData(){
        this.deathCount = 0;
        this.userName = "Player";
    }
}
