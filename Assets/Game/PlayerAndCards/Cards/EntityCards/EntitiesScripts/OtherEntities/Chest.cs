using UnityEngine;

public class Chest : EntityCard
{
    public override void Death()
    {
        //TODO: Логика добавления карт
        Debug.Log("Add cards");
        base.Death();
    }
}