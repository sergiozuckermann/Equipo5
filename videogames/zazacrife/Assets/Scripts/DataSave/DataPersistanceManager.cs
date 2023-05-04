//Made by Zaza Team
// Description: This script is used to manage the data persistance.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistance> dataPersistanceObjects;
    public static DataPersistanceManager instance { get; private set;}
    private FileDataHandler dataHandler;

    
    private void Awake(){
        if(instance != null)
        {
            Debug.LogError("Found more than one persistance manager in this scene");
        }
        instance = this;
    }

    // Start will serve as the constructor
    private void Start(){
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }
    
    public void NewGame(){

    }

    //This function is used to load the game.
    public void LoadGame(){
        this.gameData = dataHandler.Load();
        if(this.gameData == null){
            Debug.LogError("No game data found");
            NewGame();
        }
        foreach(IDataPersistance dataPersistanceObj in dataPersistanceObjects){
            dataPersistanceObj.LoadData(gameData);
        }

    }

    //This function is used to save the game.
    public void SaveGame(){
        foreach(IDataPersistance dataPersistanceObj in dataPersistanceObjects){
            dataPersistanceObj.SaveData(ref gameData);
        }
        // Aqui para guardar 
        
        dataHandler.Save(gameData);
    }
        private void OnApplicationQuit(){
        SaveGame();
    }
        private List<IDataPersistance> FindAllDataPersistanceObjects(){
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
