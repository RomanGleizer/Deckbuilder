
public class Cavalryman : CommonEnemy, IHaveAbility
{
    private IAbility _ability;

    public void UseAbility()
    {
        _ability.Use();
    }
}
