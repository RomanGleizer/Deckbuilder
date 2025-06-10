using UnityEngine;

public class EventLevelBootstrap : MonoBehaviour
{
    [SerializeField] private EventUIManager eventUI;

    void Awake()
    {
        var prog = PlayerProgressionManager.I.Progression;
        eventUI.Init(prog);
    }
}
