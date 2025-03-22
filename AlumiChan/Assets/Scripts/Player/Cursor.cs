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
    
    private float selectCooldown = 0.2f; // ˜A‘±“ü—Í‚ð–h‚®ŽžŠÔ
    private float lastSelectTime = -1f;

    private float distance;

    private void CusorMove()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction.x = -1.0f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            direction.x = 0.0f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction.x = 1.0f;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            direction.x = 0.0f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction.y = 1.0f;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            direction.y = 0.0f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            direction.y = -1.0f;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            direction.y = 0.0f;
        }
        
        rigidbody.velocity = direction * moveSpeed;
        // ƒvƒŒƒCƒ„[‚©‚ç‚Ì‹——£
        Vector3 offset = transform.position - player.transform.position;

        // ”¼Œa‚ð’´‚¦‚½‚çClamp
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
            if (!isChoose && Input.GetKeyDown(KeyCode.Return))
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
            else if (isChoose && Input.GetKeyDown(KeyCode.Return))
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
        //?~????a???ï
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
