﻿using Table.Scripts.Entities;

public interface IAbility
{
    public bool IsCanUse { get; }
    public void Use();
}