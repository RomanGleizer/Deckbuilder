using UnityEngine;

public class CommandHandler
{
    private CommandInvoker _commandInvoker;

    private IAttacker _attacker;
    private IMover _mover;
    private ISupporter _supporter;
    private ITakerDamage _takerDamage;

    public CommandHandler(MonoBehaviour card)
    {
        _attacker = card as IAttacker;
        _mover = card as IMover;
        _supporter = card as ISupporter;
        _takerDamage = card as ITakerDamage;
    }

    public void HandleCommand(Command command)
    {
        bool isCorrectCommand = false;

        if (_attacker != null && command is AttackCommand attackCommand)
        {
            attackCommand.SetReceiver(_attacker);
            isCorrectCommand = true;
        }
        else if (_mover != null && command is MoveCommand moveCommand)
        {
            moveCommand.SetReceiver(_mover);
            isCorrectCommand = true;
        }
        else if (_supporter != null && command is SupportCommand supportCommand)
        {
            supportCommand.SetReceiver(_supporter);
            isCorrectCommand = true;
        }

        if (command.IsAddToOrder)
        {
            if (isCorrectCommand) _commandInvoker.SetCommandInQueue(command);
            else if (_takerDamage != null && command is TakeDamageCommand takeDamageCommand)
            {
                takeDamageCommand.SetReceiver(_takerDamage);

                isCorrectCommand = true;
                _commandInvoker.SetCommandAndExecute(command);
            }
        }
        
        if (!isCorrectCommand)
        {
            Debug.Log("Command rejected!");
        }
    }
}
