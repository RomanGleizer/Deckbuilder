using UnityEngine;

public class PlayerPrefsStorage : IProgressionStorage
{
    public int GetInt(string key, int defaultValue) =>
        PlayerPrefs.GetInt(key, defaultValue);
    public void SetInt(string key, int value) =>
        PlayerPrefs.SetInt(key, value);
    public void Save() => PlayerPrefs.Save();
}