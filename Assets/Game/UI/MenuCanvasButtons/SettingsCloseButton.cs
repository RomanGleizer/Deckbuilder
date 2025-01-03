using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsCloseButton : CustomButton
{
    [SerializeField] private GameObject settingCanvas;
    [SerializeField] private GameObject mainCanvas;
    protected override void OnClick()
    {
        settingCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        //Time.timeScale = 1;
    }
}
