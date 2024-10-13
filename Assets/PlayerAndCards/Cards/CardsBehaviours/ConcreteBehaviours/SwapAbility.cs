using Table.Scripts.Entities;

public class SwapAbility : IAbility
{
    private CommandFactory _commandFactory;
    private Field _field;

    public SwapAbility(CommandFactory commandFactory, Field field)
    {
        _commandFactory = commandFactory;
        _field = field;
    }

    public void UseOn(Cell cell)
    {
        _commandFactory.CreateSwapCommand(cell);
    }
}