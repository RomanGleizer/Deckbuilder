using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : CustomButton
{
    [SerializeField] private GameObject pauseCanvas;
    protected override void OnClick()
    {
        pauseCanvas.SetActive(false);
        GameObject pauseControllerGO = GameObject.Find("PauseController");
        PauseController pauseCtrl = pauseControllerGO.GetComponent<PauseController>();
        pauseCtrl.isInGame = true;
        pauseCtrl.isPaused = false;
    }
}
