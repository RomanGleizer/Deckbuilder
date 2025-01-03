using UnityEngine;

public class SettingsOpenButton : CustomButton
{
    [SerializeField] private GameObject settingCanvas;
    [SerializeField] private GameObject mainCanvas;
    protected override void OnClick()
    {
        settingCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        //Time.timeScale = 0;
    }

}
