using UnityEngine.SceneManagement;

public class CampItem : Item
{
    protected override string _itemName => "Camp";
     
    protected override void ApplyItem()
    {
        SceneManager.LoadSceneAsync(3);
    }
}