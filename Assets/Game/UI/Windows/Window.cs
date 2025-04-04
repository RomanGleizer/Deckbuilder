using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] private WindowType _type;
    public WindowType Type => _type;
    
    public void ActivateWindow()
    {
        if (gameObject.activeInHierarchy) return;
        gameObject.SetActive(true);
    }

    public void DeactivateWindow()
    {
        if (!gameObject.activeInHierarchy) return;
        gameObject.SetActive(false);
    }
}