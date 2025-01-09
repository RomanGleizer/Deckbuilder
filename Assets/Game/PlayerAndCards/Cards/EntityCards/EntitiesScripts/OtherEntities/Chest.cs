using UnityEngine;

public class Chest : EntityCard
{
    public override void Init()
    {
        base.Init();
        _indicators.UpdateIndicators(0, 0);
    }
    
    public override void Death()
    {
        //TODO: Логика добавления карт
        Debug.Log("Add cards");
        base.Death();
    }
}