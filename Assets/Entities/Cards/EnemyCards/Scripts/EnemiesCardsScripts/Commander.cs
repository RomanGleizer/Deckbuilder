

public class Commander : EnemyCard, IMover, ITakerDamage, ISupporter
{
    #region Behaviours

    private ISupportBh _supportBh;
    private ITakeDamageBh _takeDamageBh;
    private IMoveBh _moveBh;

    #endregion

    public bool IsCanMove => _moveBh.IsCanMove;

    public void Move()
    {
        _moveBh.Move();
    }

    public void Support()
    {
        _supportBh.Support();
    }

    public void TakeDamage(int damage)
    {
        _takeDamageBh.TakeDamage(damage);
    }
}