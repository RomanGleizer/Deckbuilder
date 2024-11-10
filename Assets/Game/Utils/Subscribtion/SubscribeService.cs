using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

/// <summary>
/// Service that automatically subscribe and unsubscribes the transferred ISubscribable
/// </summary>
public class SubscribeService : MonoBehaviour
{
    private List<ISubscribable> _subscribables = new List<ISubscribable>();

    public void AddSubscribable(ISubscribable subscribable)
    {
        _subscribables.Add(subscribable);
    }

    public void RemoveSubscribable(ISubscribable subscribable)
    {
        if (_subscribables.Contains(subscribable))
        {
            _subscribables.Remove(subscribable);
        }
        else throw new System.ArgumentNullException("Subscribable " + subscribable.GetType() + " doesn't contain in container!");
    }

    private void SubscribeAll()
    {
        foreach (var subscribable in _subscribables)
        {
            subscribable.Subscribe();
        }
    }

    private void UnsubscribeAll()
    {
        var subscribablesCopy = new List<ISubscribable>(_subscribables);

        foreach (var subscribable in subscribablesCopy)
        {
            subscribable.Unsubscribe();
        }
    }

    private void Awake()
    {
        SubscribeAll();
    }

    private void OnDestroy()
    {
        UnsubscribeAll();
    }
}
