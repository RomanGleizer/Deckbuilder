public class ValueCancelToken
{
    public int Value { get; private set; }
    public bool IsCancellationRequest { get; private set; } 

    public ValueCancelToken(int value)
    {
        Value = value;
    }

    public void Cancel(int value)
    {
        if (Value == value)
        {
            IsCancellationRequest = true;
        }
    }

    public void Release()
    {
        IsCancellationRequest = false;
    }
}
