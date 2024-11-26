public interface IAttacker
{
    public void Attack();
}

public interface IHavePriorityCommand
{
    public Command CreatePriorityCommand();
}