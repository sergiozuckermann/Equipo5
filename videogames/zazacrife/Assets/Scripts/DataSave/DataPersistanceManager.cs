using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class DataPersistanceManager : MonoBehaviour
{
    private GameData gameData;
    private List<IDataPersistance> dataPersistanceObjects;
    public static DataPersistanceManager instance { get; private set;}

    private void Awake(){
        if(instance != null)
        {
            Debug.LogError("Found more than one persistance manager in this scene");
        }
        instance = this;
    }
    private void Start(){
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }
    
    public void NewGame(){

    }
    public void LoadGame(){
        if(this.gameData == null){
            Debug.LogError("No game data found");
            NewGame();
        }
        foreach(IDataPersistance dataPersistanceObj in dataPersistanceObjects){
            dataPersistanceObj.LoadData(gameData);
        }
        Debug.Log("Loaded death count = " + gameData.deathCount);
        Debug.Log("Name = " + gameData.userName);
    }
    public void SaveGame(){
        foreach(IDataPersistance dataPersistanceObj in dataPersistanceObjects){
            dataPersistanceObj.SaveData(ref gameData);
        }
    }
    private void OnApplicationQuit(){
        SaveGame();
    }
    private List<IDataPersistance> FindAllDataPersistanceObjects(){
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
