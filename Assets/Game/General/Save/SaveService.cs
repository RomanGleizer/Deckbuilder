using System.IO;
using UnityEngine;

using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int MapPointIndex = 0;
    public UnlockedPoints UnlockedPoints = new UnlockedPoints();
}

[Serializable]
public class UnlockedPoints
{
    public List<int> Points = new List<int>() {0, 1, 2, 3};
}

public static class SaveService
{
    public static SaveData SaveData {get; private set;}
    private const string SAVE_FILE_PATH = "Data.json";

    private static bool _isLoaded = false;

    public static void Save()
    {
        var json = JsonUtility.ToJson(SaveData);
        File.WriteAllText(Application.streamingAssetsPath + "/" + SAVE_FILE_PATH, json); 
    }

    public static void Load()
    {
        if (_isLoaded) return;

        try
        {
            var json = File.ReadAllText(Application.streamingAssetsPath + "/" + SAVE_FILE_PATH);
            if (string.IsNullOrEmpty(json)) SaveData = new SaveData();
            else SaveData = JsonUtility.FromJson<SaveData>(json);
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError("File not found!");
            File.Create(Application.streamingAssetsPath + "/" + SAVE_FILE_PATH);
            SaveData = new SaveData();
        }
        
        _isLoaded = true;
    }
}