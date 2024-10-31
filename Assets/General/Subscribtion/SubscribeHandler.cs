using System;
using UnityEngine;
using Zenject;

/// <summary>
/// Handler that simplifies subscribing and unsubscribing
/// </summary>
public class SubscribeHandler : ISubscribable
{
    private SubscribeService _subscribeService;

    private Action SubscribeAction;
    private Action UnsubscribeAction;

    [Inject]
    private void Construct(SubscribeService subscribeService)
    {
        _subscribeService = subscribeService;
        _subscribeService.AddSubscribable(this);

        if (SubscribeAction != null) Subscribe();
    }

    public SubscribeHandler()
    {

    }

    public SubscribeHandler(Action subscribeAction, Action unsubscribeAction)
    {
        SetSubscribeActions(subscribeAction, unsubscribeAction);
    }

    public void SetSubscribeActions(Action subscribeAction, Action unsubscribeAction)
    {
        SubscribeAction = subscribeAction;
        UnsubscribeAction = unsubscribeAction;

        if (_subscribeService) Subscribe();
    }

    public void Subscribe()
    {
        SubscribeAction?.Invoke();
    }

    public void Unsubscribe()
    {
        UnsubscribeAction?.Invoke();
        _subscribeService?.RemoveSubscribable(this);
    }
}