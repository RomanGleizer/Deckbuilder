using Game.PlayerAndCards.Cards.PlayerCards;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCardsContainer : MonoBehaviour
{
    [SerializeField] private PlayerCardData[] _cardsDatas;
    private PlayerCardData[] _cardsDatasInstances;

    public void Init()
    {
        _cardsDatasInstances = new PlayerCardData[_cardsDatas.Length];
        for (int i = 0; i < _cardsDatas.Length; ++i)
        {
            _cardsDatasInstances[i] = ScriptableObject.Instantiate(_cardsDatas[i]);
        }
    }

    public PlayerCardData[] GetCardsData()
    {
        return _cardsDatasInstances;
    }

    public PlayerCardData GetRandomCardData()
    {
        var randomCard = _cardsDatasInstances[Random.Range(0, _cardsDatasInstances.Length)];
        return randomCard;
    }

    public PlayerCardData GetCardData(PlayerCardNames cardName)
    {
        foreach (var cardData in _cardsDatasInstances)
        {
            if (cardData.Name == cardName) return cardData;
        }
        
        throw new System.Exception("No card found with name: " + cardName);
    }
}