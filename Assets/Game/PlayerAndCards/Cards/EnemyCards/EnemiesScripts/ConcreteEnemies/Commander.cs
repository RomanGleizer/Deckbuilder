using System.Threading.Tasks;

public class Commander : EnemyCard, IAsyncSupporter
{
    protected SupporterEnemyData _supporterEnemyData;
    protected IAsyncSupportBh _supportBh;

    public override void Init()
    {
        _supporterEnemyData = TypeChanger.ChangeObjectTypeWithException<EnemyData, SupporterEnemyData>(_enemyData);

        base.Init();
    }

    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _supportBh = _instantiator.Instantiate<RepeatAttackSupportBh>();
    }

    public override void CreatePriorityCommand()
    {
        _commandFactory.CreateAsyncSupportCommand(this, PosInOrderType.Last);
    }

    public async Task Support()
    {
        await _supportBh.Support();
    }
}