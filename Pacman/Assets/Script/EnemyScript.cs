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
    private bool isBeingKilled = false;
    PlayerState playerState = PlayerState.Normal;

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
                if (playerState == PlayerState.Invincible)
                {
                    enemyState = EnemyState.Week;
                }
                break;

            case EnemyState.Week:
                Freeze();
                if (isBeingKilled)
                {
                    StartCoroutine(Killed());
                    enemyState = EnemyState.Dead;
                }
                if (playerState == PlayerState.Normal)
                {
                    enemyState = EnemyState.Week;
                }
                break;
               

            case EnemyState.Dead:
                //killEnemy();
                if (isBeingKilled)
                {
                    isBeingKilled = false;
                    enemyState = EnemyState.Normal;
                }
                break;
        }
    }

    public void killEnemy()
    {
        StartCoroutine(Killed());
    }

    private void PlayerChange(PlayerState state)
    {
        playerState = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(enemyState == EnemyState.Week)
            {
                isBeingKilled = true;
                //enemyState = EnemyState.Dead;
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

        agent.speed = speed; //the coroutine needs to inform state that it is exiting to normal
        rend.material = normal_mat;
    }
    private enum EnemyState
    {
        Normal,
        Week,
        Dead
    }
}
