using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public GameObject uiGameOver;
    public AudioClip jumpers;
    public AudioClip pain;
    public AudioClip stampGet;

    //Hearts (not currently in use)
    //public GameObject Heart_1;
    //public GameObject Heart_2;
    //public GameObject Heart_3;
    //public GameObject canvas;

    public TMP_Text stampAmountUI;

    public float runSpeed = 40f;

    public int Lives = 3;
    public int Stamps = 0;

    public float IFrames = 0;
    float horizontalMove = 0f;
    bool Jump = false;
    bool Crouch = false;
    public bool IsAlive = true;

    void Respawn()
    {
        //Let player move again and hide game over screen
        IsAlive = true;
        animator.SetBool("Alive", true);
        uiGameOver.SetActive(false);

        Vector3 pos = transform.position;
        pos.x = -10;
        pos.y = -0;
        transform.position = pos;
  
    }
    
    //void SpawnEnemies()
    //{
    //    GameObject.Find("Bunni").
    //}

    void DamageCheck()
    {
        //Play damage sound
        GetComponent<AudioSource>().PlayOneShot(pain);

        if (Lives >= 2)
        {
            Lives -= 1;
            HealthMonitor.HealthValue -= 1;
            //healthAmountUI.text = (Lives).ToString();
        }
        else if (Lives == 1)
        {
            //healthAmountUI.text = "0";
            //Show dead animation
            HealthMonitor.HealthValue -= 1;
            animator.SetBool("Alive", false);
            //Stop the player from being able to move
            horizontalMove = 0f;
            IsAlive = false;
            //Show game over screen
            uiGameOver.SetActive(true);
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
            if (IFrames >=0)
            {
                IFrames -= 1 * Time.deltaTime;
            }
            if (IFrames < 0)
            {
                IFrames = 0;
            }
            
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

        if (Input.GetKeyDown(KeyCode.Space) && IsAlive == false)
        {
            //Respawn();
            Lives = 3;
            HealthMonitor.HealthValue = 3;
            SceneManager.LoadScene(2);
            //healthAmountUI.text = (Lives).ToString();
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, Crouch, Jump);
        Jump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y >= 0f)
        //{

        if (collision.gameObject.tag == "Enemy" && IFrames == 0)
        {
            DamageCheck();
            IFrames = 0.7f;
            Debug.Log("Damaged");
        }

        //Instant kill, mostly used when falling off the map
        if (collision.gameObject.tag == "Instadeath")
        {
            Lives = 1;
            DamageCheck();
        }

        //Taking the player to next level/ End of demo screen
        if (collision.gameObject.tag == "Level ender")
        {
            SceneManager.LoadScene(3);
        }

        if (collision.gameObject.tag == "Stamp")
        {

            //Play sound
            GetComponent<AudioSource>().PlayOneShot(stampGet);

            collision.gameObject.SetActive(false);
            Stamps += 1;
            stampAmountUI.text = (Stamps).ToString();
        }


        //}
        //if (collision.gameObject.tag == "Health")
        //    Debug.Log("Healed");
    }
}


//Code not bein used but will be revisited at some point:

////Give the player hearts
//var createImage = Instantiate(Heart_1) as GameObject;
//createImage.transform.SetParent(canvas.transform, false);
//createImage = Instantiate(Heart_2) as GameObject;
//createImage.transform.SetParent(canvas.transform, false);
//createImage = Instantiate(Heart_3) as GameObject;
//createImage.transform.SetParent(canvas.transform, false);
