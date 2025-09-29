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
    private bool isPlayerPowerUp = false;

    private bool isDead = false;

    [SerializeField]
    float speed = 7;

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
    }

    public void FreezeEnemy()
    {
        StartCoroutine(Freeze());
    }
    public void killEnemy()
    {
        StartCoroutine(Killed());
    }

    private void PlayerChange(PlayerState state)
    {
        if(state == PlayerState.Invincible)
        {
            isPlayerPowerUp = true;
            FreezeEnemy();
        }
        else if(state == PlayerState.Normal)
        {
            isPlayerPowerUp = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(isPlayerPowerUp == true)
            {
                killEnemy();
            }
            else {
                GameStateNotifier.GameStateChange(GameState.Reset);
            }
        }
    }

    private System.Collections.IEnumerator Freeze()
    {
        agent.speed = 0f;
        rend.material = week_mat;

        yield return new WaitForSeconds(3f);
        if (isDead == false)
        {
            agent.speed = speed;
            rend.material = normal_mat;
        }       
    }
    private System.Collections.IEnumerator Killed()
    {
        isDead = true;
        agent.enabled = false;
        transform.position = new Vector3(0f, 0.5f, -3.86f);
        agent.enabled = true;
        agent.speed = 0f;
        yield return new WaitForSeconds(5f);

        isDead = false;
        agent.speed = speed;
        rend.material = normal_mat;
    }
}
