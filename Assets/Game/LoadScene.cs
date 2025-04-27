using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static void Load(int index)
    {
        SceneManager.LoadScene(index);
    }
}
