using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HpIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _maxHp;
    public void UpdateIndicator(int value)
    {
        _text.text = value.ToString() + '/' + _maxHp.ToString() + " жизней";
    }

    public void UpdateIndicator(int value, int maxValue)
    {
        _maxHp = maxValue;
        _text.text = value.ToString() + '/' + _maxHp.ToString() + " жизней";
    }
}
