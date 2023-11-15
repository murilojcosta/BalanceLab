using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        directionX = 1;
    }
    private void Update()
    {
        CheckFloor();
        CheckWall();
    }

    private void FixedUpdate()
    {
        if (!isOnDestination)
        {
            //transform.Translate(direction * speed * Time.deltaTime);
            rb.velocity = new Vector2(directionX * speed, rb.velocity.y);
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if ((destinationLayer.value & (1 << collision.transform.gameObject.layer)) > 0)        
        {
            isOnDestination = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((destinationLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            isOnDestination = false;
        }
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
