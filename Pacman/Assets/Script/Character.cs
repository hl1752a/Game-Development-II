using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private EnemyScript enemyScript;
    [SerializeField]
    private float Speed = 5f;
    [SerializeField]
    private ScoreManager scoreManager;

    private bool isPowerUp = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        characterController.Move(move * Time.deltaTime * Speed);
    }
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = 0f;
        transform.position = pos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            scoreManager.AddScore();
            if (scoreManager.score == 7600)
            {
                RestartGame();
            }
        }
        else if (other.gameObject.CompareTag("PowerUp"))
        {
            StartCoroutine(PowerUp());
            enemyScript.FreezeEnemy();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            if(isPowerUp == true)
            {
                enemyScript.killEnemy();
            }
            RestartGame();
        }
    }
    private System.Collections.IEnumerator PowerUp()
    {

        isPowerUp = true;
        yield return new WaitForSeconds(3f);
        isPowerUp = false;
    }
    public void RestartGame()
    {
        // reload the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
