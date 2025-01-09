using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityCardsIndicators 
{
    private TextMeshPro _hpIndicator;
    private TextMeshPro _shieldIndicator;
    
    public EntityCardsIndicators(TextMeshPro hpIndicator, TextMeshPro shieldIndicator, int hp, int shield)
    {
        _hpIndicator = hpIndicator;
        _shieldIndicator = shieldIndicator;
        
        UpdateIndicators(hp, shield);
    }

    public void UpdateIndicators(int hp, int shield)
    {
        _hpIndicator.SetText(hp.ToString());
        _shieldIndicator.SetText(shield.ToString());
    }
}
