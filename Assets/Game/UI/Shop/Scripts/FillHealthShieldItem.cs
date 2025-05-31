using UnityEngine;

public class FillHealthShieldItem : Item
{
    [SerializeField] private PlayerData _playerData;
     
    protected override string _itemName => "Fill Health, Shield";
    private const string KMaxH = "UPG_MaxHealth";
    private const string KShield = "UPG_Shield";

    protected override void ApplyItem()
    {
        var maxHealth = PlayerPrefs.GetInt(KMaxH, 20);
        var maxShield = PlayerPrefs.GetInt(KShield, 15);
          
        _playerData.Health = maxHealth;
        _playerData.SetShield(maxShield);
    }
}