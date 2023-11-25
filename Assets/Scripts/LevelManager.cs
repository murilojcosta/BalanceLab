using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private RobotDestination[] robotDestinations;
    [SerializeField] private ExitDoor exitDoor;

    public bool IsDoorOpened {  get; private set; }

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }



    public void ControlDoor()
    {
        bool allDestinationsCompleted = true;

        foreach (RobotDestination robotDestination in robotDestinations)
        {
            if (!robotDestination.Completed())
            {
                allDestinationsCompleted = false;
            }
        }

        IsDoorOpened = allDestinationsCompleted;   
        
        if(IsDoorOpened )
        {
            exitDoor.OpenDoor();
        }
    }
}
