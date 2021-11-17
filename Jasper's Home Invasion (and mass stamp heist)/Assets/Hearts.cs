using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player"))
        {
            if (PlayerMovement.Lives < 3)
            {
                Debug.Log("Im below 3");
                PlayerMovement.Lives += 1;
                HealthMonitor.HealthValue += 1;
            }
            Destroy(gameObject);
        }
    }
}



//((HealthMonitor.HealthValue < 3) && (PlayerMovement.Lives < 3))
// ^^ Dont think this is needed but just incase

