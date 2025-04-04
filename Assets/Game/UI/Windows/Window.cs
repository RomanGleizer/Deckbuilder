using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] private WindowType _type;
    public WindowType Type => _type;
    
    public virtual void ActivateWindow()
    {
        if (gameObject.activeInHierarchy) return;
        gameObject.SetActive(true);
    }

    public virtual void DeactivateWindow()
    {
        if (!gameObject.activeInHierarchy) return;
        gameObject.SetActive(false);
    }
}