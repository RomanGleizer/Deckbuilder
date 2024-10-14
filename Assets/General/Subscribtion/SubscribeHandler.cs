using System;

/// <summary>
/// Handler that simplifies subscribing and unsubscribing
/// </summary>
public class SubscribeHandler : ISubscribable
{
    private SubscribeService _subscribeService;

    private Action SubscribeAction;
    private Action UnsubscribeAction;

    public SubscribeHandler(Action subscribeAction, Action unsubscribeAction)
    {
        SubscribeAction = subscribeAction;
        UnsubscribeAction = unsubscribeAction;
    }

    private void Construct(SubscribeService subscribeService) // Add Zenject
    {
        _subscribeService = subscribeService;
        _subscribeService.AddSubscribable(this);

        Subscribe();
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