using Table.Scripts.Entities;

public class SpawnAttackBh : ISpecialAttackBh // Pioneer special attack
{
    //private IEnemyFactory _enemyFactory; // Factory with snare spawn
    private Field _field;

    private Cell _currentCell; 

    public SpawnAttackBh(Cell currentCell)
    {
        _currentCell = currentCell;
    }

    public void Attack()
    {
        // _field.GetRandomCellExcept(_currentCell);
        // _enemyFactory.Spawn(_currentCell);
    }
}
