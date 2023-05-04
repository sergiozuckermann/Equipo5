// Description: Class to serialize a dictionary.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SerialDiccionary<TKey, TValue>: Dictionary<TKey,TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();
    
    //Save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach(KeyValuePair<TKey,TValue> pair in this){
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    //Load dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();
        for(int i = 0; i < keys.Count; i++){
            this.Add(keys[i],values[i]);
        }
    }
}
