using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;
using static UnityEngine.InputSystem.InputAction;

public class InputControl : MonoBehaviour
{
    //private PlayerInput playerInput;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        //playerInput = GetComponent<PlayerInput>();  
        player = GetComponent<Player>();
    }

    public void OnMove(CallbackContext context)
    {
        player.DirectionX = context.ReadValue<Vector2>().x;
    }

    public void OnJump(CallbackContext context)
    {
        if (context.started)
        {
            player.DoJump();
        }
    }

}
