using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;


public class Cursor : MonoBehaviour
{
    //�����o�[�ϐ�
    [SerializeField]
    [Header("Player")]
    private GameObject player;

    private Rigidbody2D rigidbody;

    [SerializeField]
    [Header("�J�[�\���ړ��̑���")]
    private float moveSpeed = 5.0f;

    Vector3 direction = Vector3.zero;

 
    [SerializeField]
    [Header("�J�[�\���̈ړ��͈͐���")]
    private GameObject Circle; 
    private float radius;
    bool isChoose;
    private GameObject square;
    //�J�[�\�����䁕����
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

        if (Vector3.Distance(transform.position, player.transform.position) <= radius)
        {
            rigidbody.velocity = direction * moveSpeed;
        }
        else
        {
            // direction: �v���C���[�����݈ʒu �̕����i�����߂������j
            Vector3 fromPlayerToCurrent = (transform.position - player.transform.position).normalized;

            // �v���C���[���甼�a�������̈ʒu�ɐ���
            transform.position = player.transform.position + fromPlayerToCurrent * radius;

            // velocity ���~�߂Ă����Ɨǂ�
            rigidbody.velocity = Vector3.zero;
        }



    }

    private void Select()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

            Vector2 center = transform.position;
            float radius = 0.2f;
            LayerMask mask = LayerMask.GetMask("Block"); 

            Collider2D hit = Physics2D.OverlapCircle(center, radius, mask);

            if (hit != null)
            {
                Debug.Log("�G���͈͓��ɂ���: " + hit.name);
                square = hit.gameObject;
                isChoose = true;
            }



        }

    }

    private void BlockMove()
    {
        if(isChoose&&square!= null) {
            square.transform.position = transform.position;
        }

    }

    // Start is called before the first frame update
    void Start()
    {   
        //�~�̔��a���擾
        radius = Circle.transform.localScale.x / 2;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        CusorMove();
        Select();
        BlockMove();
    }
}
