using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private List<ICommand> conmmand = new List<ICommand>();
    private float recordTime = 5f;
    private float timer;

    public bool isReplaying { get; private set; }

    void Update()
    {
        if (!isReplaying)
        {
            timer += Time.deltaTime;
            // keep only last 5 seconds
            //conmmand.RemoveAll(cmd => timer - cmd.TimeStamp > recordTime);
        }
    }

    public void AddCommand(ICommand command)
    {
        conmmand.Add(command);
    }

    public void StartReplay()
    {
        if (!isReplaying)
            StartCoroutine(ReplayRoutine());
    }

    private IEnumerator ReplayRoutine()
    {

        isReplaying = true;
        if (conmmand.Count == 0)
        {
            isReplaying = false;
            yield break;
        }

        float startTime = Time.time;
        float firstTime = conmmand[0].TimeStamp;

        ICommand cmd = conmmand.conmmand.RemoveAt(conmmand.Count - 1); // reomve last element
        foreach (var cmd in conmmand)
        {
            float waitTime = cmd.TimeStamp - firstTime;
            yield return new WaitForSeconds(waitTime - (Time.time - startTime));
            cmd.Undo();
        }

        isReplaying = false;
    }
}
