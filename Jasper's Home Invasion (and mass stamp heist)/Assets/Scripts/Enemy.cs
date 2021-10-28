using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Object enemyRef;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        enemyRef = Resources.Load("Bunni");
        //Set the tag of this GameObject to Player
        gameObject.tag = "Enemy";
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
}