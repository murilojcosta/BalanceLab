using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Robot : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float radiusToCheckFloor;
    [SerializeField] private float radiusToCheckWall;
    [SerializeField] private LayerMask destinationLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float directionX;
    [SerializeField] private Transform pointToCheckWall;
    [SerializeField] private Transform pointToCheckFloor;    

    private bool isOnDestination;
    private RobotDestination robotDestination;

    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        directionX = 1;
    }
    private void Update()
    {
        CheckFloor();
        CheckWall();
        EnterDestination();
    }

    private void FixedUpdate()
    {                    
        rb.velocity = new Vector2(directionX * GetSpeed(), rb.velocity.y);     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {     
        if ((destinationLayer.value & (1 << collision.transform.gameObject.layer)) > 0)        
        {            
            if(collision.TryGetComponent<RobotDestination>(out robotDestination))
            {
                isOnDestination = true;                
                speed = 0;
            }            
        }
    }

    private float GetSpeed()
    {
        if (isOnDestination)
        {
            return 0;
        }
        else
        { 
            return speed; 
        }
    }
    private void EnterDestination()
    {
        if (isOnDestination && Vector2.Distance(transform.position, robotDestination.transform.position) > 0.02f)
        {
            transform.Translate(Time.deltaTime,0,0,robotDestination.transform);
            //Debug.Log("robotDestination.transform.position " + robotDestination.transform.position);
            //Debug.Log("robotDestination.transform.localPosition " + robotDestination.transform.localPosition);
            //Debug.Log("transform.position " + transform.position);
            //Debug.Log("transform.localPosition" + transform.localPosition);

            Debug.Log("distance"+Vector2.Distance(transform.position, robotDestination.transform.position)  );

            if (Vector2.Distance(transform.position, robotDestination.transform.position) < 0.02f)
            {
                Debug.Log("Reach");
                robotDestination.OpenDoor();
                StartCoroutine(DoEnterDestination());
            }
        }        
    }

    private IEnumerator DoEnterDestination()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("Enter");
        StartCoroutine(DoCloseDoorDestination());
    }

    private IEnumerator DoCloseDoorDestination()
    {
        yield return new WaitForSeconds(0.8f);
        robotDestination.CloseDoor();
        StartCoroutine(DoInactive());
    }

    private IEnumerator DoInactive()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void CheckFloor()
    {
        Collider2D hit = Physics2D.OverlapCircle(pointToCheckFloor.position, radiusToCheckFloor, groundLayer);

        if (hit == null)
        {
            ChangeDirection();
        }
    }

    private void CheckWall()
    {
        Collider2D hit = Physics2D.OverlapCircle(pointToCheckWall.position, radiusToCheckWall, groundLayer);

        if (hit != null)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        if(directionX == 1)
        {
            directionX = -1;

            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            directionX = 1;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void OnDrawGizmos()
    {        
        Gizmos.DrawWireSphere(pointToCheckWall.position, radiusToCheckWall);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pointToCheckFloor.position, radiusToCheckFloor);
       
    }
}
