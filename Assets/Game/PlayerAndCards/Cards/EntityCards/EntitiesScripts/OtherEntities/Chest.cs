using UnityEngine;
using Zenject;

public class Chest : EntityCard
{
    public override void Init()
    {
        base.Init();
        _indicators.UpdateIndicators(0, 0);
    }

    public override void OnMouseDown()
    {
        if (CurrentCell.ColumnId != 0) return;  
        Death();
    }
    
    public override void Death()
    {
        _windowActivator.ActivateWindow(WindowType.Chest);
        base.Death();
    }
}