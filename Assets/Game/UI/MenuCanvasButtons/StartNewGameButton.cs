using UnityEngine.SceneManagement;

public class StartNewGameButton : CustomButton
{
    protected override void OnClick()
    {
        SaveService.DeleteSavings();
        SceneManager.LoadScene(1);
    }
}