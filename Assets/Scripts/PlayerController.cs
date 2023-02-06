using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;
using System;

public class PlayerController : NetworkBehaviour
{
    Animator animator;
    CharacterController characterController;
    Chat chatObj;

    public float speed = 6.0f;

    public float rotationSpeed = 900f;
    public float jumpSpeed = 10f;
    public float ySpeed = 0;
    public float gravity = 20f;

    [SyncVar]
    public string nameInGame;

    public override void OnStartLocalPlayer()
    {
        //Set Camera
        Camera.main.GetComponent<CameraX>().SetTarget(transform);
        chatObj = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Chat>();

        //Register here
        NetworkClient.RegisterHandler<ChatMessage>(OnChatMessage);
    }

    private void OnChatMessage(ChatMessage chatMessageObj)
    {
        chatObj.SetChatText(chatMessageObj.message);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(nameInGame);
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            chatObj.SendChat(nameInGame);
        }

        float vertical = 0;
        float horizontal = 0;

        if(!chatObj.IsFocus())
        {
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");
        }

        if (vertical != 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        if (ySpeed > 0)
            ySpeed += Physics.gravity.y * Time.deltaTime;
        else
            ySpeed = 0;



        if (characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !chatObj.IsFocus())
            {
                ySpeed = jumpSpeed;
            }
        }


        Vector3 velocity = speed * vertical * characterController.transform.forward;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);   
        
        //transform.position += speed * vertical * transform.forward * Time.deltaTime;

        if (horizontal != 0)
        {
            if(horizontal > 0)
            {
                transform.RotateAround(transform.position, transform.up, 120f * Time.deltaTime);
            }
            else
            {
                transform.RotateAround(transform.position, transform.up, -120f * Time.deltaTime);
            }
        }
        

    }
}
