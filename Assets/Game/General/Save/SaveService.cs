﻿using System.IO;
using UnityEngine;

using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int MapPointIndex = 0;
    public UnlockedPoints UnlockedPoints = new UnlockedPoints();

    public int Coins = 0;
    
    public Deck Deck = new Deck();
    public float Volume = 0.5f;
}

[Serializable]
public class UnlockedPoints
{
    public List<int> Points = new List<int>() {0, 1};
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

    public static void DeleteSavings()
    {
        var volume = SaveData.Volume;
        SaveData = new SaveData() {Volume = volume};
        PlayerPrefs.DeleteAll();
        Save();
    }
}