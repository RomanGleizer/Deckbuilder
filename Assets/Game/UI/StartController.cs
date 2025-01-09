using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject settingsCanvas;
    public bool isSettings = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isSettings)
        {            
            settingsCanvas.SetActive(false);
            mainCanvas.SetActive(true);
            isSettings = false;
        }
    }
}
