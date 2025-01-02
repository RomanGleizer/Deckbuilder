using UnityEngine;

public class SettingsOpenButton : CustomButton
{
    [SerializeField] private GameObject settingCanvas;
    [SerializeField] private bool isInGame;
    protected override void OnClick()
    {
        settingCanvas.SetActive(true);
        if (isInGame) OpenInGame();
        if (!isInGame) OpenInStart();
    }

    private void OpenInGame()
    {
        Time.timeScale = 0;
    }

    private void OpenInStart()
    {
        GameObject mainCanvas = GameObject.Find("MenuCanvas");
        mainCanvas.SetActive(false);
    }

}
