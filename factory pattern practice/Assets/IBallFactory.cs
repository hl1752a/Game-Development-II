using UnityEngine;

public interface IBallFactory
{
    GameObject CreateBall(Vector2 position);
}