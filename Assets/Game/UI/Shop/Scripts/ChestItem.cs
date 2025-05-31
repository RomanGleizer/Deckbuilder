using Zenject;

public class ChestItem : Item
{
    protected override string _itemName => "Chest";

    private WindowActivator _windowActivator;

    [Inject]
    private void Construct(WindowActivator windowActivator)
    {
        _windowActivator = windowActivator;
    }
     
    protected override void ApplyItem()
    {
        _windowActivator.ActivateWindow(WindowType.Chest);
    }
}