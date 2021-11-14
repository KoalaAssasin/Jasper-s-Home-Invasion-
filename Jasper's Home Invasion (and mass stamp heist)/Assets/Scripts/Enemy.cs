using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Object enemyRef;

    private bool movingLeft = true;
    private bool movingRight = false;

    public float Timer = 2.0f;
    public Vector3 pos;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        enemyRef = Resources.Load("Bunni");
        //Set the tag of this GameObject to Player
        //gameObject.tag = "Enemy";
    }

    private void Awake()
    {
        pos = gameObject.transform.position;
    }

    void Respawn()
    {
        GameObject enemyClone = (GameObject)Instantiate(enemyRef);
        enemyClone.transform.position = new Vector3(UnityEngine.Random.Range(startPos.x - 1, startPos.x + 1), startPos.y, startPos.z);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Rigidbody2D> ().velocity.y <= 0f)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(10.0f, 10.0f);
                //Destroy(gameObject);
                gameObject.SetActive(false);

               // Invoke("Respawn", 5);

            }
        }
    }

    private void Update()
    {
        if (gameObject.tag == "EnemyMoving")
        {
            if (Timer != 0 && movingRight)
            {
                pos.x += 0.01f;
                gameObject.transform.position = pos;
                Timer -= Time.deltaTime;
            }

            if (Timer != 0 && movingLeft)
            {
                pos.x -= 0.01f;
                gameObject.transform.position = pos;
                Timer -= Time.deltaTime;
            }

            if (Timer <= 0 && movingRight)
            {
                movingRight = false;
                movingLeft = true;
                Timer = 2.0f;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (Timer <= 0 && movingLeft)
            {
                movingRight = true;
                movingLeft = false;
                Timer = 2.0f;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

        }

        if (gameObject.tag == "EnemyIdle")
        {
         
        }
    }

}