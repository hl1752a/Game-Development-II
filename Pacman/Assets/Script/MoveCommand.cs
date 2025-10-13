using UnityEngine;

public class MoveCommand : ICommand
{
    private CharacterController characterController;
    private Vector3 direction;
    private float speed;
    private Vector3 position;
    public float TimeStamp { get; private set; }

    public MoveCommand(CharacterController controller, Vector3 direction, float speed, float timeStamp)
    {
        characterController = controller;
        this.direction = direction;
        this.speed = speed;
        TimeStamp = timeStamp;
    }

    public void Execute()
    {
        characterController.Move(direction * speed * Time.deltaTime);
    }

    public void Undo()
    {
        characterController.Move(-direction * speed * Time.deltaTime);
    }
}
