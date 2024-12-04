namespace Game.PlayerAndCards.PlayerScripts.Interfaces
{
    public interface IStunnable
    {
        void Stun(int duration);
        bool IsStunned { get; }
    }
}