using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public GameObject player;

    private HashSet<GameObject> blocksInRange = new HashSet<GameObject>();
    private Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("block"))
        {
            SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
            if (sr != null && !originalColors.ContainsKey(other.gameObject))
            {
                originalColors[other.gameObject] = sr.color;
                sr.color = Color.yellow;
                blocksInRange.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("block"))
        {
            if (originalColors.TryGetValue(other.gameObject, out Color originalColor))
            {
                SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = originalColor;
                }

                originalColors.Remove(other.gameObject);
                blocksInRange.Remove(other.gameObject);
            }
        }
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
