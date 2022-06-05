using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<PlayerHealth>().playerHealth <= 0)
        {
            Destroy(gameObject);
            if (gameObject.tag == "player")
            {

            }
        }
    }
}
