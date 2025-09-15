using UnityEngine;
using UnityEngine.AI;

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

    private bool isDead = false;

    [SerializeField]
    float speed = 7;

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
