public interface IHaveShield
{
    public bool HasShield { get; }

    public void AddShield(int value);
    public void BreakShield();
}