using UnityEngine;

public class RedBallFactory : IBallFactory
{
    private GameObject prefab;

    public RedBallFactory()
    {
        prefab = Resources.Load<GameObject>("Prefabs/RedBall");
    }

    public GameObject CreateBall(Vector2 position)
    {
        return Object.Instantiate(prefab, position, Quaternion.identity);
    }
}