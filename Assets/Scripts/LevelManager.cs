using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int totalRobots;
    [SerializeField] private int robotsInDestination;

    public bool IsDoorOpened {  get; private set; }

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void AddRobotInDestination(int savedRobots)
    {
        robotsInDestination += savedRobots;
        ControlDoor();
    }

    private void ControlDoor()
    {
        IsDoorOpened = (robotsInDestination >= totalRobots);        
    }
}
