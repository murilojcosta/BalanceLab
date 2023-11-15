using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RobotDestination : MonoBehaviour
{
    [SerializeField] private LayerMask layerRobot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((layerRobot.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            LevelManager.instance.AddRobotInDestination(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((layerRobot.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            LevelManager.instance.AddRobotInDestination(-1);
        }    
    }
}
