using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.UI.Image;

public class EnemyMove : MonoBehaviour
{
    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;         //�n�ʃ`�F�b�N
    [SerializeField] protected float groundCheckDistance;     //�`�F�b�N����
    [SerializeField] protected Transform wallCheck;           //�ǃ`�F�b�N
    [SerializeField] protected float wallCheckDistance;       //�`�F�b�N����
    [SerializeField] protected LayerMask whatIsGround;        //���C���[�ݒ�

    public float speed = 1.0f;  //���x
    public LayerMask checklayer;//�ǂ̃��C���[�̂��̂Ɠ������������f����
    private float facingDir => transform.forward.z;
    public float length;        //�ǂɑ΂��铖���蔻��̑傫��

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y; 
        if (!IsGroundDetected() || IsWallDetected())
        {
            y = (transform.rotation.y == 0f) ? 180.0f : 0.0f;//�ǂɓ���������180�x��]���鏈��
            transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
        }        
        transform.position += new Vector3(transform.forward.z * speed * Time.deltaTime, 0, 0);//�ړ�����
        Debug.Log(facingDir);
    }

    // ���n�ʃ`�F�b�N
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    // ���ǃ`�F�b�N
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, new Vector2(facingDir,0), wallCheckDistance, whatIsGround);
    
    //�`�F�b�N�̒���Raycast�����Ĕ���Ə����̌Ăяo�����ꏏ�ɍs���Ă���

    //�����̏����̒��ŕ`�悳��Ă���������ɓ����蔻����v�Z���Ă���
    protected void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
    }
}