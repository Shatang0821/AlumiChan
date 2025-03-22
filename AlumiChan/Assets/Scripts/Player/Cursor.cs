using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;


public class Cursor : MonoBehaviour
{
    [SerializeField]
    [Header("Player")]
    private GameObject player;

    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    [SerializeField]
    
    private float moveSpeed = 5.0f;

    Vector3 direction = Vector3.zero;

 
    [SerializeField]
    private GameObject Circle; 
    private float radius;
    bool isChoose;
    private GameObject square;
    
    private float selectCooldown = 0.2f; // 連続入力を防ぐ時間
    private float lastSelectTime = -1f;

    private float distance;

    private void CusorMove()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction.x = -1.0f;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            direction.x = 0.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1.0f;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            direction.x = 0.0f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1.0f;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            direction.y = 0.0f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1.0f;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            direction.y = 0.0f;
        }
        
        rigidbody.velocity = direction * moveSpeed;
        // プレイヤーからの距離
        Vector3 offset = transform.position - player.transform.position;

        // 半径を超えたらClamp
        if (offset.magnitude > radius) 
        {
            offset = offset.normalized * radius; 
        }

        transform.position = player.transform.position + offset;
    }

    private void LateUpdate()
    {
        // プレイヤーからの距離
        Vector3 offset = transform.position - player.transform.position;

        // 半径を超えたらClamp
        if (offset.magnitude > radius) 
        {
            offset = offset.normalized * radius; 
        }

        transform.position = player.transform.position + offset;
    }

    private void Select()
    {
        if (Time.time - lastSelectTime > selectCooldown)
        {
            if (!isChoose && Input.GetKeyDown(KeyCode.Z))
            {
                Vector2 center = transform.position;
                float radius = 0.2f;
                LayerMask mask = LayerMask.GetMask("Block");

                Collider2D hit = Physics2D.OverlapCircle(center, radius, mask);

                if (hit != null)
                {
                    square = hit.gameObject;
                    isChoose = true;
                    collider.enabled = true;

                    square.GetComponent<SpriteRenderer>().color = Color.red;
                  
                    
                    square.GetComponent<BoxCollider2D>().enabled = false;
                    lastSelectTime = Time.time;
                }
            }
            else if (isChoose && Input.GetKeyDown(KeyCode.Z))
            {
                UnSelect();
            }
        }
    }

    private void UnSelect()
    {
        if (square == null)
            return;
        isChoose = false;
        square.GetComponent<BoxCollider2D>().enabled = true;
        collider.enabled = false;
        square.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        square.GetComponent<SpriteRenderer>().color = Color.white;

        square = null;
        lastSelectTime = Time.time;
    }

    private void BlockMove()
    {
        if(isChoose&&square!= null) {
            square.transform.position = transform.position;
        }

    }

    private void Check()
    {
        if(Vector3.Distance(player.transform.position, transform.position) <= 1.5f)
        {
            UnSelect() ;
        }
    }


    // Start is called before the first frame update
    void Start()
    {   
        //?~????a???擾
        radius = Circle.transform.localScale.x / 2;
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        CusorMove();
        Select();
        BlockMove();
        Check();
    }
}
