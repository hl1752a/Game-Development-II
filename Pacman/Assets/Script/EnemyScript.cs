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
        float temp = 0f;
        temp = agent.speed;

        agent.speed = 0f;
        rend.material = week_mat;

        yield return new WaitForSeconds(3f);

        agent.speed = temp;
        rend.material = normal_mat;
    }
    private System.Collections.IEnumerator Killed()
    {
        transform.position = new Vector3(0f, 0.5f, -3.86f);
        agent.speed = 0f;
        yield return new WaitForSeconds(5f);
    }
}
