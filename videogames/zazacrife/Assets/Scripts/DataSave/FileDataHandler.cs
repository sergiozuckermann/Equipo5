using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
   private string dataDirPath = "";
   private string dataFileName = "";

   public FileDataHandler(string dataDirPath, string dataFileName){
    this.dataDirPath = dataDirPath;
    this.dataFileName = dataFileName;
   }
   public GameData Load(){

   }
   public void Save(GameData data){
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try{
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file " + fullPath + "\n" + e);
        }

   }
}
