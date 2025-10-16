using UnityEngine;

public class GreenBallFactory : IBallFactory
{
    private GameObject prefab;

    public GreenBallFactory()
    {
        prefab = Resources.Load<GameObject>("Prefabs/GreenBall");
    }

    public GameObject CreateBall(Vector2 position)
    {
        return Object.Instantiate(prefab, position, Quaternion.identity);
    }
}