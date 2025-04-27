using UnityEngine;
using System.Collections.Generic;

public class EventUIManager : MonoBehaviour
{
    [SerializeField] private EventType eventType;
    [SerializeField] private RectTransform optionsContainer;
    [SerializeField] private GameObject optionPrefab;

    private PlayerProgression _prog;

    public void Init(PlayerProgression prog)
    {
        _prog = prog;
        ShowOptions();
    }

    private void ShowOptions()
    {
        foreach (Transform child in optionsContainer)
            Destroy(child.gameObject);

        List<UpgradeType> opts;
        if (eventType == EventType.Choice)
        {
            opts = _prog.GetRandomUpgradeOptions();
        }
        else
        {
            opts = _prog.GetAvailableUpgrades();
            opts.Add(UpgradeType.RestoreHealth);
        }

        int count = opts.Count;
        float width = optionsContainer.rect.width;
        float spacing = width / (count + 1);

        for (int i = 0; i < count; i++)
        {
            var go = Instantiate(optionPrefab, optionsContainer);
            var rt = go.GetComponent<RectTransform>();
            rt.localScale = Vector3.one;
            float x = -width / 2f + spacing * (i + 1);
            rt.anchoredPosition = new Vector2(x, rt.anchoredPosition.y);

            var optComp = go.GetComponent<UpgradeOption>();
            optComp.Init(opts[i], _prog);
        }

        optionsContainer.gameObject.SetActive(true);
    }
}
