using UnityEngine.SceneManagement;

public class ReturnToMapButton : CustomButton
{
    protected override void OnClick()
    {
        SceneManager.LoadSceneAsync(1);
    }
}