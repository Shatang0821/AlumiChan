using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rigidbody2D;
    
    /*============変数管理==========*/
    [SerializeField]
	[Header("移動の速さ")]
	private float moveSpeed = 5f;
	[SerializeField]
	[Header("ジャンプ力")]
	private float jumpPower = 10.0f;
	[SerializeField]
	[Header("ノックバック力")]
	private Vector2 knockbackForce = new Vector2(10.0f,0); // Knockbackの強さ
	[SerializeField]
	[Header("ノックバックのの持続時間")]
	private float knockbackDuration = 0.2f;// Knockbackの持続時間
	/*==============================*/

	/*============地面変数管理==========*/
	[Header("地面のタグ名")]
	private string groundTag = "Ground"; // 地面のタグ名.
	[SerializeField]
	[Header("地面に接しているかどうか")]
	/*==============================*/

	/*============電気弾==========*/
	[Header("弾のオブジェクト")]
	public GameObject AuBulletObj;//弾のオブジェクト.
	Vector3 bulletPoint;  //弾の位置.
	/*============================*/


	private bool Ground = false;      // 地面に接しているかどうか.
	private bool Jumping = false;     // ジャンプ中かどうかを追跡するフラグ.
	private bool KnockedBack = false; // ノックバック中かどうかを管理.
	private bool LiSkillJigi = false; // リチウムスキルが使えるのかのフラグ.
	private bool hasExecuted = false; //　Executedが使えているかどうか.
	private bool electricityjigi = false;//電気が使えているか.


	void Start()
	{
		// Rigidbody2Dコンポーネントを取得.
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		// 地面判定を更新.
		//CheckGrounded();

		// プレイヤーの移動とジャンプ.
		Move();
		Jump();
		Excute();
		
	}

	/// <summary>
	/// プレイヤー移動.
	/// </summary>
	private void Move()
	{
		// キー入力を直接チェック.
		float x = 0;
	if (!KnockedBack) { 
			if (Input.GetKey(KeyCode.A))
			{
				x = -1;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				x = 1;
			}

		  if (x != 0)
		  {
			// 移動方向に応じてキャラクターの向きを変更.
			transform.localScale = new Vector3(x * 1, 1, 1);
			// 移動処理 - moveSpeedを使用.
			transform.position += new Vector3(x * moveSpeed * Time.deltaTime, 0, 0);
		   }
	    }
	}

	/// <summary>
	/// ジャンプ.
	/// </summary>
	private void Jump()
	{
		// ジャンプ操作: 地面に接している場合のみ.
		if (Input.GetKeyDown(KeyCode.Space) && !Jumping)
		{
			// ジャンプ力を加える.
			rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
			Jumping = true;
		}
	}
	
	/// <summary>
	/// ジャンプ力管理
	/// </summary>
	public void SetJumpPower()
	{
		jumpPower = 20;
	}
	/// <summary>
	/// リチウムスキルリターン.
	/// </summary>
	public void ResetPower()
	{
		jumpPower = 10;
	}

	/// <summary>
	/// 金のスキル.
	/// </summary>
	public void electricity()
	{

	}
	/// <summary>
	/// 電気だまの管理.
	/// </summary>
	public void electricityball()
	{
		// 弾の生成
		Instantiate(AuBulletObj);
	}


	private void Knockback(Vector2 knockbackDuration)
	{
		rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
		//ノックバックの中を設定.
		KnockedBack = true;
		if (Jumping)
		{
			//ノックバックの力を加える.
			rigidbody2D.AddForce(knockbackDuration * knockbackForce, ForceMode2D.Impulse);
		}
		else
		{
			rigidbody2D.AddForce(knockbackDuration * knockbackForce, ForceMode2D.Impulse);
		}

		//ノックバックの持続時間後にフラグを戻す.
		StartCoroutine(ResetKnockback());
	}
	private IEnumerator ResetKnockback()
	{
		yield return new WaitForSeconds(knockbackDuration);
		KnockedBack = false;
		rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
	}



	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag(groundTag))
		{
			// 地面と接触したらジャンプ可能に戻す.
			Jumping = false;
		}
		//エネミーに当たったらノックバックする.
		if (collision.gameObject.CompareTag("enemy"))
		{
			Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
			Knockback(knockbackDirection);
		}

		//死亡.
		if(collision.gameObject.CompareTag("desu"))
		{
			Destroy(this.gameObject);
		}

		//リチウムスキル.
		if (collision.gameObject.CompareTag("Li"))
		{
			LiSkillJigi = true;
		}
		//電気をまとう
		if (collision.gameObject.CompareTag("Au"))
		{
			electricityjigi = true;
		}

	}
	

	/// <summary>
	/// 死亡.
	/// </summary>
	private void Death()
	{

	}

	/// <summary>
	/// アイテム.
	/// </summary>
	public void Excute()
	{


		if (Input.GetKeyDown(KeyCode.Q) && LiSkillJigi == true)
		{
			if(!hasExecuted)
		    {
				SkillLi skillLi = GetComponent<SkillLi>();
				skillLi.Excute();
				hasExecuted = true;
			}
			else
			{
				ResetPower();
				hasExecuted = false;
			}

			
		}

		if(Input.GetKeyDown(KeyCode.K) && electricityjigi == true)
		{
			KnockedBack = false;
			knockbackDuration = 0f;

		}
		if (Input.GetKeyDown(KeyCode.R) && electricityjigi == true)
		{
			electricityball();
		}
	}

}
