using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RobotDestination : MonoBehaviour
{    
    [SerializeField] private int totalRobots;
    [SerializeField] private int currentRobots;

    [SerializeField] private Animator animator;

    public void AddRobot()
    {
        currentRobots++;
        LevelManager.instance.ControlDoor();
    }

    public void OpenDoor()
    {
        animator.SetTrigger("Open");
    }

    public void CloseDoor()
    {
        animator.SetTrigger("Close");
    }

    public bool Completed()
    {
        return currentRobots == totalRobots;
    }
}
