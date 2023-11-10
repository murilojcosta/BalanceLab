using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.IsTouchingLayers(3))
        {
            player.IsOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.IsOnGround = false;
    }
}
