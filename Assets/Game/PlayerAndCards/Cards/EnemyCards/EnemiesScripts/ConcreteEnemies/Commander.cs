using System.Threading;
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
        var command = _commandFactory.CreateAsyncSupportCommand(this, PosInOrderType.Last);
        command.SetVisual(HiglightActivingEnemy, UnhiglightActivingEnemy);
    }

    public async Task Support(CancellationToken token)
    {
        await _supportBh.Support(token);
    }
}