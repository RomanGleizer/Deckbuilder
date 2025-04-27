using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    private void Awake()
    {
        SaveService.Load();
    }
}