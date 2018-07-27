using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance
    {
        get { return instance; }
    }

    private int temporaryScore; 
    public int TemporaryScore { set { temporaryScore = value;  }  get { return temporaryScore; } }

    private string path;
    private PlayerData playerData; 
    public PlayerData PlayerData { get { return playerData; } }

    public void Awake()
    {
        if (instance == null)
            instance = this; 
        else
        {
            if (instance != this)
                Destroy(gameObject); 
        }

        path = Application.persistentDataPath + "/PlayerData.json";
        LoadData();
    }

    // Use this for initialization
    public void LoadData()
    {
        string jsonString;
        if (File.Exists(path))
        {
            jsonString = File.ReadAllText(path);
            playerData = JsonUtility.FromJson<PlayerData>(jsonString);
        }else
        {
            playerData = new PlayerData();
            SaveData();
            
        }
    }

    public void SaveData()
    {
        string jsonString; 

        jsonString = JsonUtility.ToJson(playerData);
        File.WriteAllText(path, jsonString);
        return; 
    }
     
}
