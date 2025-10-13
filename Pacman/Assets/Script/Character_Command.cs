using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public CommandManager commandManager;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (commandManager.isReplaying) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(h, 0f, v).normalized;

        if (move.magnitude > 0f)
        {
            controller.Move(move * speed * Time.deltaTime);

            var cmd = new MoveCommand(controller, move, speed, Time.time);
            commandManager.AddCommand(cmd);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            commandManager.StartReplay();
        }
    }
}
