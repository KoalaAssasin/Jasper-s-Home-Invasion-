﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool Jump = false;
    bool Crouch = false;

    void Start()
    {
        //Set the tag of this GameObject to Player
        gameObject.tag = "Player";
    }

    // Update is called once per frame
    void Update()
    {
       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButton("Jump"))
        {
            Jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            Crouch = true;
        } else if (Input.GetButton("Crouch"))
        {
            Crouch = false;
        }

    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, Crouch, Jump);
        Jump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Damaged");
            Destroy(this.gameObject);
        }
        //if (collision.gameObject.tag == "Health")
        //    Debug.Log("Healed");
    }
}