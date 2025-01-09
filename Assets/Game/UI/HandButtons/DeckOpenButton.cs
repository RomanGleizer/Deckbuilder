using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckOpenButton : CustomButton
{
    [SerializeField] private GameObject deckWindow;
    protected override void OnClick()
    {
        deckWindow.SetActive(true);
    }
}
