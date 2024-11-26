using System.Collections.Generic;

public class ValueCancelTokenSource
{
    private List<ValueCancelToken> _tokens = new List<ValueCancelToken>();

    public ValueCancelToken GetToken(int value)
    {
        foreach (ValueCancelToken token in _tokens)
        {
            if (token.Value == value) return token;
        }
        
        var newToken = new ValueCancelToken(value);
        _tokens.Add(newToken);
        return newToken;
    }

    public void Cancel(int value)
    {
        foreach (ValueCancelToken token in _tokens)
        {
            if (token.Value == value)
            {
                token.Cancel(value);
                return;
            }
        }

        var newToken = new ValueCancelToken(value);
        _tokens.Add(newToken);
        newToken.Cancel(value);
    }

    public void Release()
    {
        foreach (ValueCancelToken token in _tokens)
        {
            token.Release();
        }
    }
}