using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class UpgradeOption : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button selectButton;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private UpgradeType type;
    private PlayerProgression progression;

    public void Init(UpgradeType upgradeType, PlayerProgression prog)
    {
        type = upgradeType;
        progression = prog;

        var sprite = Resources.Load<Sprite>($"UpgradeIcons/{type}");
        if (sprite != null)
            icon.sprite = sprite;
        else
            Debug.LogWarning($"������ ��� �������� '{type}' �� ������� � Images/UpgradeIcons");

        descriptionText.text = GetDescription(type);

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(OnSelect);
        selectButton.interactable = true;
    }

    private void OnSelect()
    {
        progression.ApplyUpgrade(type);
        LoadScene.Load(1);
    }

    private string GetDescription(UpgradeType t) => t switch
    {
        UpgradeType.MaxHealth => "+3 � ������������� ��������",
        UpgradeType.Shield => "+1 � �����",
        UpgradeType.HandSize => "+1 � ������� ����",
        UpgradeType.RedrawCount => "+1 � ��������� ����",
        UpgradeType.RestoreHealth => "��������� ������������ HP",
        _ => ""
    };
}
