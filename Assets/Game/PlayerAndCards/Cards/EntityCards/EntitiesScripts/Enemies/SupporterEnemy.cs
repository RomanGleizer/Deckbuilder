public abstract class SupporterEnemy : EnemyCard, ISupporter
{
    protected SupporterEnemyData _supporterEnemyData;
    protected ISupportBh _supportBh;

    public override void Init()
    {
        _supporterEnemyData = TypeChanger.ChangeObjectTypeWithException<EntityData, SupporterEnemyData>(_entityData);

        base.Init();
    }

    public void Support()
    {
        _supportBh.Support();
    }
}