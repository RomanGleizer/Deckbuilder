using UnityEngine;
using Zenject;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject settingsCanvas;
    public bool isInGame = true;
    public bool isPaused = false;
    public bool isSettings = false;
    
    private AudioPlayer _audioPlayer;

    [Inject]
    private void Construct(AudioPlayer audioPlayer)
    {
        _audioPlayer = audioPlayer;
    }
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            AllBoolIsFasle();
            pauseCanvas.SetActive(false);
            _audioPlayer.PlayAudio("main");
            isInGame = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isSettings)
        {
            AllBoolIsFasle();
            PauseIsActive();
            settingsCanvas.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isInGame)
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
        _audioPlayer.PlayAudio("menu");
        pauseCanvas.SetActive(true);
        isPaused = true;
    }
}
