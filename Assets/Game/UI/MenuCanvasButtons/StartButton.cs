using UnityEngine.SceneManagement;

public class StartButton : CustomButton
{
    protected override void OnClick()
    {
        SceneManager.LoadScene(1);
    }

}
