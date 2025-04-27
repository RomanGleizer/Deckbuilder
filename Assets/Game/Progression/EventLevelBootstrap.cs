// EventLevelBootstrap.cs
using UnityEngine;
using Game.PlayerAndCards.PlayerScripts;

public class EventLevelBootstrap : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private EventUIManager campfireUI;

    void Awake()
    {
        var dataInstance = ScriptableObject.Instantiate(playerData);

        var prog = new PlayerProgression(
            dataInstance,
            new UnityRandomProvider(),
            new PlayerPrefsStorage()
        );

        LoadPlayerDataSingleton.I.DataInstance = dataInstance;
        LoadPlayerDataSingleton.I.Progression = prog;

        campfireUI.Init(prog);
    }
}
