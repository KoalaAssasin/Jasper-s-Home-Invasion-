using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public GameObject uiGameOver;
    public AudioClip jumpers;

    public float runSpeed = 40f;

    public int Lives = 3;

    float horizontalMove = 0f;
    bool Jump = false;
    bool Crouch = false;
    public bool IsAlive = true;

    void Respawn()
    {
        IsAlive = true;
        animator.SetBool("Alive", true);
        uiGameOver.SetActive(false);

        Vector3 pos = transform.position;
        pos.x = -10;
        pos.y = -0;
        transform.position = pos;
  
    }

    void DamageCheck()
    {
        if (Lives >= 2)
        {
            Lives -= 1;
        }
        else if (Lives == 1)
        {
            animator.SetBool("Alive", false);
            IsAlive = false;
            uiGameOver.SetActive(true);
            horizontalMove = 0f;
        }
    }

    void Start()
    {
        //Set the tag of this GameObject to Player
        gameObject.tag = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive == true)
        {

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButton("Jump"))
            {
                Jump = true;
            }

            //For jump sound effect
            if (Input.GetButtonDown("Jump"))
            {
                GetComponent<AudioSource>().PlayOneShot(jumpers);
            }

            if (Input.GetButtonDown("Crouch"))
            {
                Crouch = true;
            }
            else if (Input.GetButton("Crouch"))
            {
                Crouch = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Respawn();
            Lives = 3;
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
            DamageCheck();
            Debug.Log("Damaged");
        }
        //if (collision.gameObject.tag == "Health")
        //    Debug.Log("Healed");
    }
}
