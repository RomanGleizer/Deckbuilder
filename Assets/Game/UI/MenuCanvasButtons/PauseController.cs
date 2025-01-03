using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
    private bool isInGame = true;
    private bool isPaused = false;
    private bool isSettings = false;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            AllBoolIsFasle();
            pauseCanvas.SetActive(false);
            isInGame = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isSettings)
        {
            AllBoolIsFasle();
            PauseIsActive();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isInGame)
        {
            AllBoolIsFasle();
            PauseIsActive();
        }        
    }

    private void AllBoolIsFasle()
    {
        isInGame = false;
        isPaused = false;
        isSettings = false;
    }

    private void PauseIsActive()
    {
        pauseCanvas.SetActive(true);
        isPaused = true;
    }
}
