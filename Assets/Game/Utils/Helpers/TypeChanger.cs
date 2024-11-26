public static class TypeChanger
{
    /// <summary>
    /// Change object's type from TIN to TOUT
    /// </summary>
    /// <returns> Changed object</returns>

    public static TOUT ChangeObjectTypeWithException<TIN, TOUT>(TIN currentObject)
    {
        if (currentObject is TOUT type)
        {
            return type;
        }
        else
        {
            throw new System.ArgumentException(typeof(TIN) + " should have " + typeof(TOUT) + " type. Not " + currentObject.GetType() + " type!");
        }
    }

    public static TOUT ChangeObjectTypeWithNull<TIN, TOUT>(TIN currentObject)
    {
        if (currentObject is TOUT type)
        {
            return type;
        }
        return default(TOUT);
    }
}