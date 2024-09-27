public abstract class SupporterEnemy : EnemyCard, ISupporter
{
    protected SupporterEnemyData _supporterEnemyData;
    protected ISupportBh _supportBh;

    protected override void Init()
    {
        _supporterEnemyData = TypeChanger.ChangeObjectType<EnemyData, SupporterEnemyData>(_enemyData);

        base.Init();
    }

    public void Support()
    {
        _supportBh.Support();
    }
}