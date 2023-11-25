using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RobotDestination : MonoBehaviour
{    
    [SerializeField] private int totalRobots;
    [SerializeField] private int currentRobots;

    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI panelText;

    private void Start()
    {
        currentRobots = 0;
        UpdatePanelText();
    }

    private void UpdatePanelText()
    {
        panelText.text = currentRobots.ToString() + '/' + totalRobots.ToString();
    }

    public void AddRobot()
    {
        currentRobots++;
        UpdatePanelText();

        LevelManager.instance.ControlDoor();        
    }

    public void OpenDoor()
    {
        animator.SetTrigger("Open");
    }

    public void CloseDoor()
    {
        animator.SetTrigger("Close");
        StartCoroutine(DoAddRobot());
    }

    private IEnumerator DoAddRobot()
    {
        yield return new WaitForSeconds(0.8f);
        AddRobot();
    }

    public bool Completed()
    {
        return currentRobots == totalRobots;
    }
}
