using Game.PlayerAndCards.Cards.PlayerCards;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInDesk : MonoBehaviour
{
    [SerializeField] private PlayerCardData _playerCardData;
    [SerializeField] private int _countOfCard;
    [SerializeField] private TextMeshProUGUI _cardCountText;
    [SerializeField] private Image _cardImage;


    public PlayerCardData PlayerCardData => _playerCardData;
    public int CountOfCard
    {
        get
        {
            return _countOfCard;    
        }
        set
        {
            _countOfCard = value;
        }
    }

    private void Start()
    {
        _cardImage.sprite = _playerCardData.Sprite;
        _cardCountText.text = "x" + _countOfCard;
    }
}
