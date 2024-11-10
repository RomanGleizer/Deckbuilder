using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private int _maxEnergy;
    public void UpdateIndicator(int value)
    {
        _text.text = value.ToString() + '/' + _maxEnergy.ToString();
    }

    //public void UpdateIndicator(int value, int maxValue)
    //{

    //}
}
