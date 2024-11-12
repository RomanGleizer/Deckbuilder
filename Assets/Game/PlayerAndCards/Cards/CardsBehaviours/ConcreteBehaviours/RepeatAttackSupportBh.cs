using UnityEngine;
using System.Threading.Tasks;
using Zenject;
using System.Threading;

public class RepeatAttackSupportBh : IAsyncSupportBh
{
    private CommandInvoker _invoker;

    [Inject]
    private void Construct(CommandInvoker invoker)
    {
        _invoker = invoker;
    }

    public async Task Support(CancellationToken token)
    {
        Debug.Log("RepeatAttackCommands");
        await _invoker.RepeatAttackCommands(token);
    }
}