using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    void Start()
    {
        //Set the tag of this GameObject to Player
        gameObject.tag = "Enemy";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Rigidbody2D> ().velocity.y <= 0f)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(10.0f, 10.0f);
                Destroy(gameObject);
            }
        }
    }
}