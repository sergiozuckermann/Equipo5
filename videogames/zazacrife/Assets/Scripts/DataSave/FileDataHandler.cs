//Made by Zaza Team
// Description: This script is used to handle the file data.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
   private string dataDirPath = "";
   private string dataFileName = "";

   //Constructor
   public FileDataHandler(string dataDirPath, string dataFileName)
   {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
   }

    //This function is used to load the data from the file.
   public GameData Load()
   {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if(File.Exists(fullPath)){
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }

            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file " + fullPath + "\n" + e);
            }
        }
        return loadedData;
   }

   //This function is used to save the data to the file.
   public void Save(GameData data){
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try{
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data,true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file " + fullPath + "\n" + e);
        }

   }
}
