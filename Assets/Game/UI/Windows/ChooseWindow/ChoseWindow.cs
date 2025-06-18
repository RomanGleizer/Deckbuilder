using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChoseWindow : Window
{
    [Header("UI References")]
    
    [SerializeField] private Image _imagePlacement;
    [SerializeField] private ChooseButton[] _buttons;
    
    [SerializeField] private TextMeshProUGUI _eventTittlePlacement;
    [SerializeField] private TextMeshProUGUI _descriptionPlacement;
    
    [Header("Event Data")]
    [SerializeField] private string _eventTittle;
    [SerializeField] private string _description;
    [SerializeField] private string[] _answers;
    [SerializeField] private Sprite _sprite;

    public void Init(string eventTittle, string description, string[] answers, Sprite sprite)
    {
        _eventTittle = eventTittle;
        _description = description;
        _answers = answers;
        _sprite = sprite;
        FillEvent();
    }
    
    private void FillEvent()
    {
        _eventTittlePlacement.text = _eventTittle;
        _descriptionPlacement.text = _description;
        
        for (int i = 0; i < _answers.Length; ++i)
        {
            _buttons[i].Init(_answers[i]);
        }

        _imagePlacement.sprite = _sprite;
    }

    public override void ActivateWindow()
    {
        base.ActivateWindow();
        FillEvent();
    }
}
