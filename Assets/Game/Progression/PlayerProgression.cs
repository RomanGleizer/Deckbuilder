using Game.PlayerAndCards.PlayerScripts;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerProgression : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerData playerData;

    private Dictionary<UpgradeType, int> upgradeLevels = new()
    {
        { UpgradeType.MaxHealth, 0 },
        { UpgradeType.Shield, 0 },
        { UpgradeType.HandSize, 0 },
        { UpgradeType.RedrawCount, 0 }
    };

    private const int maxUpgrades = 3;

    public List<UpgradeType> GetAvailableUpgrades()
    {
        var available = new List<UpgradeType>();
        foreach (var upgrade in upgradeLevels)
        {
            if (upgrade.Value < maxUpgrades)
                available.Add(upgrade.Key);
        }
        return available;
    }

    public List<UpgradeType> GetRandomUpgradeOptions()
    {
        var available = GetAvailableUpgrades();
        var options = new List<UpgradeType>();

        if (available.Count <= 2)
        {
            options.AddRange(available);
        }
        else
        {
            while (options.Count < 2)
            {
                var index = Random.Range(0, available.Count);
                var option = available[index];
                if (!options.Contains(option))
                {
                    options.Add(option);
                }
            }
        }
        return options;
    }

    public void ApplyUpgrade(UpgradeType upgrade)
    {
        if (upgradeLevels[upgrade] >= maxUpgrades)
        {
            return;
        }

        upgradeLevels[upgrade]++;
        switch (upgrade)
        {
            case UpgradeType.MaxHealth:
                playerData.Health += 3;
                break;
            case UpgradeType.Shield:
                playerData.SetShield(playerData.Shield + 1);
                break;
            case UpgradeType.HandSize:
                playerData.SetHandSize(playerData.HandSize + 1);
                break;
            case UpgradeType.RedrawCount:
                playerData.SetRedrawCount(playerData.RedrawCount + 1);
                break;
        }
    }
}