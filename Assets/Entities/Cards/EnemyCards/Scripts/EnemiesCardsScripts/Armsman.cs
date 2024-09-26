public class Armsman : IMoveBh, ISupportBh, ITakeDamageBh
{
    #region Behaviours

    private IMoveBh _moveBh;
    private ISupportBh _supportBh;
    private ITakeDamageBh _takeDamageBh;

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