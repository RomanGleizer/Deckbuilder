using UnityEngine;

public class FillHealthShield : Item
{
    [SerializeField] private PlayerData _playerData;
     
    protected override string _itemName => "Fill Health, Shield";
    private const string KMaxH = "UPG_MaxHealth";
    private const string KShield = "UPG_Shield";

    protected override void ApplyItem()
    {
        var maxHealth = PlayerPrefs.GetInt(KMaxH, 0);
        var maxShield = PlayerPrefs.GetInt(KShield, 0);
          
        _playerData.Health = maxHealth;
        _playerData.SetShield(maxShield);
    }
}