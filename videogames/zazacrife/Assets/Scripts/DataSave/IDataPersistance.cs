//Made by Zaza Team 
//This script handles the data persistance of the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This interface is used to load and save the game data.
public interface IDataPersistance
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
} 