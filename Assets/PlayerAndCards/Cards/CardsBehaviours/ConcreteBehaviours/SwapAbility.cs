using Table.Scripts.Entities;

public class SwapAbility : IAbility
{
    private CommandFactory _commandFactory;
    private Field _field;

    private CellTrackerByEnemy _cellTracker;

    public bool IsCanUse => _cellTracker.GetCurrentCell().ColumnId > 0;

    public SwapAbility(CommandFactory commandFactory, Field field, EnemyCard enemyCard)
    {
        _commandFactory = commandFactory;
        _field = field;

        _cellTracker = new CellTrackerByEnemy(enemyCard);
    }

    public void Use()
    {
        var cell = _cellTracker.GetCurrentCell();
        _commandFactory.CreateSwapCommand(cell);
    }
}