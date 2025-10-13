using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerStateNotifier;

public class Character_Observer : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private float Speed = 5f;


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
        
        if (other.gameObject.CompareTag("PowerUp"))
        {
            ItemCollectNotifier.ItemCollected(ItemType.PowerUp);
            StartCoroutine(PowerUpTimer());

        }
        if (other.gameObject.CompareTag("Coin"))
        {
            ItemCollectNotifier.ItemCollected(ItemType.Coin);

        }
    }
    private System.Collections.IEnumerator PowerUpTimer()
    {
        PlayerStateNotifier.PlayerStateChange(PlayerState.Invincible);
        yield return new WaitForSeconds(3f);
        PlayerStateNotifier.PlayerStateChange(PlayerState.Normal);
    }
}
