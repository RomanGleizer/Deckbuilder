using System.Collections.Generic;

public class PlayerProgression
{
    private readonly PlayerData _data;
    private readonly IRandomProvider _rand;
    private readonly IProgressionStorage _store;

    private readonly Dictionary<UpgradeType, int> _levels = new()
    {
        { UpgradeType.MaxHealth,   0 },
        { UpgradeType.Shield,      0 },
        { UpgradeType.HandSize,    0 },
        { UpgradeType.RedrawCount, 0 }
    };

    private const int MaxUpgrades = 3;
    private const string KMaxH = "UPG_MaxHealth";
    private const string KShield = "UPG_Shield";
    private const string KHand = "UPG_HandSize";
    private const string KRedraw = "UPG_RedrawCount";

    public PlayerProgression(
        PlayerData data,
        IRandomProvider rand,
        IProgressionStorage store)
    {
        _data = data;
        _rand = rand;
        _store = store;
        Load();
    }

    public List<UpgradeType> GetRandomUpgradeOptions()
    {
        var avail = new List<UpgradeType>();
        foreach (var kv in _levels)
            if (kv.Value < MaxUpgrades)
                avail.Add(kv.Key);

        var opts = new List<UpgradeType>();
        if (avail.Count <= 2)
            opts.AddRange(avail);
        else
            while (opts.Count < 2)
            {
                var pick = (UpgradeType)_rand.Next(0, avail.Count);
                if (!opts.Contains(pick))
                    opts.Add(pick);
            }
        return opts;
    }

    public List<UpgradeType> GetAvailableUpgrades()
    {
        var avail = new List<UpgradeType>();
        foreach (var kv in _levels)
            if (kv.Value < MaxUpgrades)
                avail.Add(kv.Key);
        return avail;
    }

    public void ApplyUpgrade(UpgradeType t)
    {
        if (t == UpgradeType.RestoreHealth)
        {
            _store.SetInt("UPG_DoRestore", 1);
            _store.Save();
            return;
        }

        if (_levels[t] >= MaxUpgrades)
            return;

        _levels[t]++;
        switch (t)
        {
            case UpgradeType.MaxHealth:
                _data.Health += 3;
                break;
            case UpgradeType.Shield:
                _data.SetShield(_data.Shield + 1);
                break;
            case UpgradeType.HandSize:
                _data.SetHandSize(_data.HandSize + 1);
                break;
            case UpgradeType.RedrawCount:
                _data.SetRedrawCount(_data.RedrawCount + 1);
                break;
        }

        Save();
    }

    private void Save()
    {
        _store.SetInt(KMaxH, _levels[UpgradeType.MaxHealth]);
        _store.SetInt(KShield, _levels[UpgradeType.Shield]);
        _store.SetInt(KHand, _levels[UpgradeType.HandSize]);
        _store.SetInt(KRedraw, _levels[UpgradeType.RedrawCount]);
        _store.Save();
    }

    private void Load()
    {
        _levels[UpgradeType.MaxHealth] = _store.GetInt(KMaxH, 0);
        _levels[UpgradeType.Shield] = _store.GetInt(KShield, 0);
        _levels[UpgradeType.HandSize] = _store.GetInt(KHand, 0);
        _levels[UpgradeType.RedrawCount] = _store.GetInt(KRedraw, 0);

        foreach (var kv in _levels)
            for (int i = 0; i < kv.Value; i++)
                switch (kv.Key)
                {
                    case UpgradeType.MaxHealth:
                        _data.Health += 3;
                        break;
                    case UpgradeType.Shield:
                        _data.SetShield(_data.Shield + 1);
                        break;
                    case UpgradeType.HandSize:
                        _data.SetHandSize(_data.HandSize + 1);
                        break;
                    case UpgradeType.RedrawCount:
                        _data.SetRedrawCount(_data.RedrawCount + 1);
                        break;
                }
    }
}
