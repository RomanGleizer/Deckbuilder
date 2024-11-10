using UnityEngine;
using Zenject;

public class CommandHandler
{
    private CommandInvoker _commandInvoker;

    private IAttacker _attacker;
    private IMoverToCell _mover;
    private ISupporter _supporter;
    private ITakerDamage _takerDamage;
    private IInvincibilable _invincibilable;
    private IHavePriorityCommand _havePriorityCommand;

    public CommandHandler(MonoBehaviour card)
    {
        _attacker = card as IAttacker;
        _mover = card as IMoverToCell;
        _supporter = card as ISupporter;
        _takerDamage = card as ITakerDamage;
        _invincibilable = card as IInvincibilable;
        _havePriorityCommand = card as IHavePriorityCommand;
    }

    [Inject]
    private void Construct(CommandInvoker commandInvoker)
    {
        _commandInvoker = commandInvoker;
    }

    public void HandleCommand(Command command) // TODO: слишком перегружен + нарушается DRY - подумать как переделать
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
        else if (_invincibilable != null && command is InvincibilityCommand invincibilityCommand)
        {
            invincibilityCommand.SetReceiver(_invincibilable);
            isCorrectCommand = true;
        }
        else if (_takerDamage != null && command is TakeDamageCommand takeDamageCommand)
        {
            takeDamageCommand.SetReceiver(_takerDamage);
            isCorrectCommand = true;
        }
        else if (_havePriorityCommand != null && command is ActionCommand actionCommand)
        {
            actionCommand.SetReceiver(_havePriorityCommand);
            isCorrectCommand = true;
        }

        if (command.IsAddToOrder && isCorrectCommand)
        {
            if (command is TakeDamageCommand takeDamageCommand || command is InvincibilityCommand invincibilityCommand)
            {
                _commandInvoker.SetCommandAndExecute(command);
            }
            else _commandInvoker.SetCommandInQueue(command);
        }
        
        if (!isCorrectCommand)
        {
            Debug.Log("Command rejected!");
        }
    }
}
