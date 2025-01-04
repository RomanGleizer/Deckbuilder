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
        if (GameObject.Find("PauseController"))
        {
            GameObject pauseCtrlGO = GameObject.Find("PauseController");
            PauseController pauseController = pauseCtrlGO.GetComponent<PauseController>();
            pauseController.isSettings = true;
            pauseController.isPaused = false;
        }

        if (GameObject.Find("StartController"))
        {
            GameObject startCtrlGO = GameObject.Find("StartController");
            StartController startController = startCtrlGO.GetComponent<StartController>();
            startController.isSettings = true;
        }
    }

}
