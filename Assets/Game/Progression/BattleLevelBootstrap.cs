using UnityEngine;
using Game.PlayerAndCards.PlayerScripts;

public class BattleLevelBootstrap : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    void Awake()
    {
        if (LoadPlayerDataSingleton.I != null && LoadPlayerDataSingleton.I.DataInstance != null)
        {
            return;
        }

        var dataInstance = ScriptableObject.Instantiate(playerData);

        var prog = new PlayerProgression(
            dataInstance,
            new UnityRandomProvider(),
            new PlayerPrefsStorage()
        );

        LoadPlayerDataSingleton.I.DataInstance = dataInstance;
        LoadPlayerDataSingleton.I.Progression = prog;
    }
}
