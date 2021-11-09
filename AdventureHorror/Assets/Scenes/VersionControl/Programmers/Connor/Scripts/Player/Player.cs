using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{


    public float mouseX;
    public float mouseY;
    public float vertical1D;
    public float horizontal1D;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Transform hand;



    public PlayerData PD;
    // public P_Input input;
    public P_Movment movment;
    public P_Inventory inventory;
    public P_Interact interact;


    ////////////////Update/Start/Awake////////////////
    ////////////////Update/Start/Awake////////////////

    public void Awake()
    {
        PD.player = this;
        //input = new P_Input(PD, this);
        movment = new P_Movment(PD);
        inventory = new P_Inventory(PD);
        interact = new P_Interact(PD);
        Cursor.visible = false; // hides cursor 
        Cursor.lockState = CursorLockMode.Confined;// locks cursor to game window
        PD.isHiding = false;
        PD.inHand = null;
    }


    void Update()
    {
        movment.UpdateCamera(mouseX, mouseY);
        if (!PD.isHiding)
        {
            movment.UpdatePosition(horizontal1D, vertical1D);
        }
    }
    ////////////////Update/Start/Awake////////////////
    ////////////////Update/Start/Awake////////////////







    #region Input Methods
    public void OnMouseLook(InputAction.CallbackContext context)
    {

        mouseX = context.ReadValue<Vector2>().x * PD.mouseSensitivity * Time.deltaTime;
        mouseY = context.ReadValue<Vector2>().y * PD.mouseSensitivity * Time.deltaTime;
    }
    public void OnHorizontalMove(InputAction.CallbackContext context)
    {
        horizontal1D = context.ReadValue<float>();

    }
    public void OnVerticaleMove(InputAction.CallbackContext context)
    {
        vertical1D = context.ReadValue<float>();

    }
    public void OnJump(InputAction.CallbackContext context)
    {
        movment.Jump();

    }
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        interact.Interact();

    }
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (PD.inHand != null)
        {
            interact.ThrowHandObj();
        }

    }
    #endregion

}


