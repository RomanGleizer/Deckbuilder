
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

    public override Command CreatePriorityCommand()
    {
        Command command;
        if (CurrentCell.ColumnId > 0)
        {
            command = _commandFactory.CreateSwapCommand(CurrentCell);
        }
        else command = _commandFactory.CreateAttackCommand(this);

        command.SetVisual(HiglightActivingEnemy, UnhiglightActivingEnemy);
        return command;
    }
}
