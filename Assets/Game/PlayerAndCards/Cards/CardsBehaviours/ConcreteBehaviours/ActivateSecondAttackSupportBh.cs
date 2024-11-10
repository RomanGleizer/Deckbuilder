public class ActivateSecondAttackSupportBh : ISupportBh
{
    private CommandInvoker _invoker;

    public void Support()
    {
        _invoker.IncreaseExecuteCount();
    }
}