using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShieldIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    public void UpdateIndicator(int value)
    {
        _text.text = value.ToString();
    }
}
