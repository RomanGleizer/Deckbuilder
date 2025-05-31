using Game.PlayerAndCards.Cards.PlayerCards;
using UnityEngine;
using Zenject;

public class CardItem : Item
{
    [SerializeField] private PlayerCardNames _playerCardNames;
    protected override string _itemName => _playerCardNames.ToString();
     
    private PlayerCardsContainer _playerCardsContainer;

    [Inject]
    private void Construct(PlayerCardsContainer playerCardsContainer)
    {
        _playerCardsContainer = playerCardsContainer;
    }
     
    protected override void ApplyItem()
    {
        var card = _playerCardsContainer.GetCardData(_playerCardNames);
        SaveService.SaveData.Deck.AddCardInDeck(card.Name, card.CountInChest);
        SaveService.Save();
    }
}