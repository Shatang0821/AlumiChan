using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public GameObject player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            foreach (Transform t in collision.transform) 
            {
                if (t.gameObject.CompareTag("block"))
                {

                }
            }
        }
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
