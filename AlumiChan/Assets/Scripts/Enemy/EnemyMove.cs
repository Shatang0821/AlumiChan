using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.UI.Image;

public class EnemyMove : MonoBehaviour
{
    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;         //地面チェック
    [SerializeField] protected float groundCheckDistance;     //チェック距離
    [SerializeField] protected Transform wallCheck;           //壁チェック
    [SerializeField] protected float wallCheckDistance;       //チェック距離
    [SerializeField] protected LayerMask whatIsGround;        //レイヤー設定

    public float speed = 1.0f;  //速度
    public LayerMask checklayer;//どのレイヤーのものと当たったか判断する
    private float facingDir => transform.forward.z;
    public float length;        //壁に対する当たり判定の大きさ

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
            y = (transform.rotation.y == 0f) ? 180.0f : 0.0f;//壁に当たったら180度回転する処理
            transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
        }        
        transform.position += new Vector3(transform.forward.z * speed * Time.deltaTime, 0, 0);//移動処理
        Debug.Log(facingDir);
    }

    // ↓地面チェック
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    // ↓壁チェック
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, new Vector2(facingDir,0), wallCheckDistance, whatIsGround);
    
    //チェックの中にRaycastを入れて判定と処理の呼び出しを一緒に行っている

    //↓この処理の中で描画されている線を元に当たり判定を計算している
    protected void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
    }
}