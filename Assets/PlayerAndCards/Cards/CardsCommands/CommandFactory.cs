public class CommandFactory
{
    public Command CreateAttackCommand()
    {
        return new AttackCommand();
    }
    
    public Command CreateSupportCommand()
    {
        return new SupportCommand();
    }
    
    public Command CreateMoveCommand()
    {
        return new MoveCommand();
    }

    public Command CreateTakeDamageCommand(int damage)
    {
        return new TakeDamageCommand(damage);
    }
}