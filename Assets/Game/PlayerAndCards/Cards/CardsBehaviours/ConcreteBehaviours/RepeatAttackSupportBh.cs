using UnityEngine;
using System.Threading.Tasks;
using Zenject;

public class RepeatAttackSupportBh : IAsyncSupportBh
{
    private CommandInvoker _invoker;

    [Inject]
    private void Construct(CommandInvoker invoker)
    {
        _invoker = invoker;
    }

    public async Task Support()
    {
        Debug.Log("RepeatAttackCommands");
        await _invoker.RepeatAttackCommands();
    }
}