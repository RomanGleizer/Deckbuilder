using UnityEngine;

public class PlayerProgressionManager : MonoBehaviour
{
    public static PlayerProgressionManager I { get; private set; }

    [SerializeField] private PlayerData playerDataSO;

    public PlayerData DataInstance { get; private set; }
    public PlayerProgression Progression { get; private set; }

    private static readonly string[] keys = {
        "UPG_MaxHealth",
        "UPG_Shield",
        "UPG_HandSize",
        "UPG_RedrawCount",
        "UPG_DoRestore"
    };

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void NewGame()
    {
        foreach (var k in keys) PlayerPrefs.DeleteKey(k);
        PlayerPrefs.Save();

        InitDataAndProgression();
    }

    public void ContinueGame()
    {
        InitDataAndProgression();
    }

    private void InitDataAndProgression()
    {
        DataInstance = ScriptableObject.Instantiate(playerDataSO);
        Progression = new PlayerProgression(
            DataInstance,
            new UnityRandomProvider(),
            new PlayerPrefsStorage()
        );
    }
}