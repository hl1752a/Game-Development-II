using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  
            return;
        }
        else
        {
            Instance = this;
        }
            
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            IBallFactory factory = BallFactoryProvider.GetRandomFactory();
            factory.CreateBall(mouseWorldPos);
        }
    }
}
