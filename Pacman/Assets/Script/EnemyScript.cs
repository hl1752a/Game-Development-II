using UnityEngine;
using UnityEngine.AI;
using static GameStateNotifier;
using static PlayerStateNotifier;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Material normal_mat;

    [SerializeField]
    private Material week_mat;

    private Renderer rend;

    [SerializeField]
    float speed = 7;

    private EnemyState enemyState = EnemyState.Normal;

    private void OnEnable()
    {
        PlayerStateNotifier.OnPlayerStateChange += PlayerChange;
    }

    private void OnDisable()
    {
        PlayerStateNotifier.OnPlayerStateChange -= PlayerChange;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        switch (enemyState)
        {
            case EnemyState.Normal:
                agent.speed = speed;
                rend.material = normal_mat;
                break;

            case EnemyState.Week:
                Freeze();
                break;

            case EnemyState.Dead:
                killEnemy();
                break;
        }
    }


    public void killEnemy()
    {
        StartCoroutine(Killed());
    }

    private void PlayerChange(PlayerState state)
    {
        if(state == PlayerState.Invincible)
        {
            enemyState = EnemyState.Week;
        }
        else if(state == PlayerState.Normal)
        {
            enemyState = EnemyState.Normal;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(enemyState == EnemyState.Week)
            {
                enemyState = EnemyState.Dead;
            }
            else {
                GameStateNotifier.GameStateChange(GameState.Reset);
            }
        }
    }

    private void Freeze()
    {
        agent.speed = 0f;
        rend.material = week_mat;   
    }

    private System.Collections.IEnumerator Killed()
    {
        enemyState = EnemyState.Normal;
        agent.enabled = false;
        transform.position = new Vector3(0f, 0.5f, -3.86f);
        agent.enabled = true;
        agent.speed = 0f;
        yield return new WaitForSeconds(5f);

        agent.speed = speed;
        rend.material = normal_mat;
    }
    private enum EnemyState
    {
        Normal,
        Week,
        Dead
    }
}
