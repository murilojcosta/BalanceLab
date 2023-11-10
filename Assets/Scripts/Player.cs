using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    [Header("Propertys")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isOnGround;    
    
    private Rigidbody2D rb;

    private float directionX;
    public float DirectionX
    {
        set
        {
            directionX = value;
        }
    }

    public bool IsOnGround
    {
        get
        {
            return isOnGround;
        }
        set
        {
            isOnGround = value;            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(directionX * speed, rb.velocity.y);

        if (directionX > 0.6)
        {
            transform.eulerAngles = new Vector3(0, 0);
        }

        if (directionX < -0.6)
        {
            transform.eulerAngles = new Vector3(0, 180);
        }
    }
    public void DoJump()
    {
        if (IsOnGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
