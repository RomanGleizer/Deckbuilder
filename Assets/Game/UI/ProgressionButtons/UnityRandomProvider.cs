using UnityEngine;

public class UnityRandomProvider : IRandomProvider
{
    public int Next(int minInclusive, int maxExclusive) =>
        Random.Range(minInclusive, maxExclusive);
}