namespace Game.PlayerAndCards.PlayerScripts.Interfaces
{
    public interface IStunnable
    {
        void Stun(float duration);
        bool IsStunned { get; }
    }
}