using UnityEngine;

public class LoadPlayerDataSingleton : MonoBehaviour
{
    public static LoadPlayerDataSingleton I { get; private set; }

    [HideInInspector] public PlayerData DataInstance;
    [HideInInspector] public PlayerProgression Progression;

    void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
