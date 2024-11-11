
public class Cavalryman : CommonEnemy, IHaveAbility
{
    private IAbility _ability;

    protected override void InitBehaviours()
    {
        _ability = _instantiator.Instantiate<SwapAbility>(new object[] {this});
        base.InitBehaviours();
    }

    public void UseAbility()
    {
        _ability.Use();
    }

    public override void CreatePriorityCommand()
    {
        if (CurrentCell.ColumnId > 0)
        {
            _commandFactory.CreateSwapCommand(CurrentCell);
        }
        else _commandFactory.CreateAttackCommand(this);
    }
}
