using Zenject;

public class RandomCardItem : Item
{
    protected override string _itemName => "RandomCard";
     
    private PlayerCardsContainer _playerCardsContainer;

    [Inject]
    private void Construct(PlayerCardsContainer playerCardsContainer)
    {
        _playerCardsContainer = playerCardsContainer;
    }
     
    protected override void ApplyItem()
    {
        var card = _playerCardsContainer.GetRandomCardData();
        SaveService.SaveData.Deck.AddCardInDeck(card.Name, card.CountInChest);
        SaveService.Save();
    }
}