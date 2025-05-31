using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public abstract class Item : MonoBehaviour
{
     [SerializeField] private int _price;
     [SerializeField] private TextMeshProUGUI _itemNameText;

     protected abstract string _itemName {get; }
     
     [SerializeField] private BuyButton _buyButton;

     private CoinsCounter _coinsCounter;
     
     [Inject]
     private void Construct(CoinsCounter coinsCounter)
     {
          _coinsCounter = coinsCounter;
     }

     protected virtual void Awake()
     {
          _buyButton.Init(_price);
          _buyButton.OnBuyClicked += Buy;
          _itemNameText.text = _itemName;
     }

     private void Buy()
     {
          if (_coinsCounter.Coins < _price) return;
          
          _coinsCounter.RemoveCoins(_price);
          ApplyItem();
     }

     protected abstract void ApplyItem();

     private void OnDestroy()
     {
          _buyButton.OnBuyClicked -= Buy;
     }
}