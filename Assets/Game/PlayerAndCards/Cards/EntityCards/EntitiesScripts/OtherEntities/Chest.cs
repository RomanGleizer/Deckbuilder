using UnityEngine;
using Zenject;

public class Chest : EntityCard
{
    private HandManager _handManager;

    [Inject]
    private void Construct(HandManager handManager)
    {
        _handManager = handManager; 
    }
    
    public override void Init()
    {
        base.Init();
        _indicators.UpdateIndicators(0, 0);
    }

    public override void OnMouseDown()
    {
        Death();
    }
    
    public override void Death()
    {
        //TODO: Добавить логику добавления карт
        base.Death();
    }
}