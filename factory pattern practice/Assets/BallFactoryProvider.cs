// BallFactoryProvider.cs
using UnityEngine;

public static class BallFactoryProvider
{
    public static IBallFactory GetRandomFactory()
    {
        if (Random.value > 0.5f)
            return new GreenBallFactory();
        else
            Debug.Log("red ball");
            return new RedBallFactory();
    }
}
