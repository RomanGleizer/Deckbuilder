public class Rock : EntityCard, ITakerDamage
{
    private int _hp;
    private ITakeDamageBh _takeDamageBh;

    public override void Init()
    {
        _hp = TypeChanger.ChangeObjectTypeWithException<EntityData, RockData>(_entityData).Hp;

        var subscribeHandler = _instantiator.Instantiate<SubscribeHandler>();
        subscribeHandler.SetSubscribeActions(Subscribe, Unsubscribe);

        _takeDamageBh = new TakeDamageBh(this);

        base.Init();
        _indicators.UpdateIndicators(_hp, 0);
    }

    public void TakeDamage(int damage)
    {
        _takeDamageBh.TakeDamage(damage, ref _hp);
        _indicators.UpdateIndicators(_hp, 0);
    }
}
