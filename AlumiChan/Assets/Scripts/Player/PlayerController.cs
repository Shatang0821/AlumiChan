using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	private Vector2 knockbackForce = new Vector2(10.0f, 0); // Knockbackの強さ
	[SerializeField]
	[Header("ノックバックのの持続時間")]
	private float knockbackDuration = 0.2f;// Knockbackの持続時間
	/*==============================*/

	/*============地面変数管理==========*/
	// ↓地面チェック
	[SerializeField] protected LayerMask whatIsGround; 
	[SerializeField] protected Transform groundCheck;         //地面チェック
	[SerializeField] protected float groundCheckDistance;     //チェック距離//レイヤー設定
	public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
	
	//[Header("地面のタグ名")]
	//private string groundTag = "Ground"; // 地面のタグ名.
	//[SerializeField]
	//[Header("地面に接しているかどうか")]
	/*==============================*/

	/*============電気弾==========*/
	[Header("弾のオブジェクト")]
	public GameObject AuBulletObj;//弾のオブジェクト.
	[SerializeField]
	[Header("弾の速度")]
	private float bulletSpeed = 10f;//弾の速度.
	[Header("弾のクールタイム(秒)")]
	[SerializeField]
	private float bulletCooldown = 1.5f; // 弾のクールタイム.
	private bool canShoot = true; // 弾が撃てるかどうか.
	Vector3 bulletPoint;                // 弾の位置.
	private float bulletDirection = 1f; // 弾の発射方向 (1: 右, -1: 左).
	/*============================*/

	[SerializeField]
	private Animator _animator;
	[SerializeField]
	private Animator _nextAnimator;
	private bool Ground = false;      // 地面に接しているかどうか.
	private bool Jumping = false;     // ジャンプ中かどうかを追跡するフラグ.
	private bool KnockedBack = false; // ノックバック中かどうかを管理.

	private SkillBase currentSkill;
	[SerializeField]
	private GameObject FeContainer;

	[SerializeField]
	private SkillUIController skillUIController;
	
	[SerializeField]
	private AudioData moveAudioData;
	[SerializeField]
	private AudioData jumpAudioData;
	void Start()
	{
		// Rigidbody2Dコンポーネントを取得.
		rigidbody2D = GetComponent<Rigidbody2D>();
		bulletPoint = transform.Find("BulletPoint").localPosition;
	}

	// Update is called once per frame
	void Update()
	{
		// 地面判定を更新.
		//CheckGrounded();

		// プレイヤーの移動とジャンプ.
		Move();
		Jump();
		ExecuteSkill();
	}

	/// <summary>
	/// プレイヤー移動.
	/// </summary>
	private void Move()
	{
		// キー入力を直接チェック.
		float x = 0;
		if (!KnockedBack)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				x = -1;
				bulletDirection = -1f; // 左向きに設定
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				x = 1;
				bulletDirection = 1f; // 右向きに設定
			}

			if (x != 0)
			{
				_animator.SetBool("Move",true);
				_nextAnimator.SetBool("Move",true);
				// 移動方向に応じてキャラクターの向きを変更.
				transform.localScale = new Vector3(x * 1, 1, 1);
				// 移動処理 - moveSpeedを使用.
				transform.position += new Vector3(x * moveSpeed * Time.deltaTime, 0, 0);
			}
			else
			{
				_animator.SetBool("Move",false);
				_nextAnimator.SetBool("Move",false);
			}
		}

		if (Jumping && rigidbody2D.velocity.y == 0)
		{
			Jumping = false;
			_animator.SetBool("Jump",false);
			_nextAnimator.SetBool("Jump",false);
		}
	}

	public void SetSkill(string skillName)
	{
		skillUIController.SetSkillHave(skillName);
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
			_animator.SetBool("Jump",true);
			_nextAnimator.SetBool("Jump",false);
			AudioManager.Instance.PlaySFX(jumpAudioData);
		}

		if (Jumping)
		{
			if (rigidbody2D.velocity.y >= 0)
			{
				_animator.SetFloat("VelocityY",1);
				_nextAnimator.SetFloat("VelocityY",1);
			}
			else
			{
				_animator.SetFloat("VelocityY",-1);
				_nextAnimator.SetFloat("VelocityY",-1);
			}
			
		}
	}

	/// <summary>
	/// ジャンプ力管理
	/// </summary>
	public void SetJumpPower()
	{
		jumpPower = 15;
	}
	/// <summary>
	/// リチウムスキルリターン.
	/// </summary>
	public void ResetPower()
	{
		jumpPower = 12;
	}

	/// <summary>
	/// 金のスキル.
	/// </summary>
	public void electricity()
	{
		// 電気をまとうスキルの実装があれば追加
	}


	public void InitElec()
	{
		KnockedBack = false;
		knockbackDuration = 0f;
	}
	/// <summary>
	/// 電気だまの管理.
	/// </summary>
	public void electricityball()
	{
		
		// クールタイム中なら撃てない
		if (!canShoot)
			return;

		// 弾を生成して発射方向を設定
		Vector3 spawnPosition = transform.position + new Vector3(bulletPoint.x * bulletDirection, bulletPoint.y, bulletPoint.z);
		GameObject bullet = Instantiate(AuBulletObj, spawnPosition, Quaternion.identity);

		// BulletControllerを取得して弾に速度を設定
		BulletController bulletController = bullet.GetComponent<BulletController>();
		if (bulletController != null)
		{
			bulletController.SetDirection(bulletDirection);
			bulletController.SetSpeed(bulletSpeed);
		}
		else
		{
			// BulletControllerがない場合はRigidbody2Dを直接操作
			Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
			if (bulletRb != null)
			{
				bulletRb.velocity = new Vector2(bulletSpeed * bulletDirection, 0);
			}
		}

		// クールタイムを開始
		StartCoroutine(BulletCooldown());
	}

	public void RestElec()
	{
		knockbackDuration = 0.2f;
	}
	
	
	/// <summary>
	/// 弾のクールタイム処理
	/// </summary>
	private IEnumerator BulletCooldown()
	{
		canShoot = false;
		yield return new WaitForSeconds(bulletCooldown);
		canShoot = true;
	}

	public void InitFe()
	{
		FeContainer.SetActive(true);
	}

	public void ResetFe()
	{
		FeContainer.SetActive(false);
	}

	private void Knockback(Vector2 knockbackDuration)
	{
		rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
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
		if (IsGroundDetected())
		{
			// 地面と接触したらジャンプ可能に戻す.
			Jumping = false;
			_animator.SetBool("Jump",false);
		}
		//エネミーに当たったらノックバックする.
		if (collision.gameObject.CompareTag("enemy"))
		{
			Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
			Knockback(knockbackDirection);
		}

		//死亡.
		if (collision.gameObject.CompareTag("desu"))
		{
			Death();
		}

		//リチウムスキル.
		if (collision.gameObject.CompareTag("Li"))
		{
			//LiSkillJigi = true;
			//currentSkill = new SkillLi();
		}
		//電気をまとう
		if (collision.gameObject.CompareTag("Au"))
		{
			//electricityjigi = true;
			//currentSkill = new SkillAu();
		}

		if (collision.gameObject.CompareTag("Fe"))
		{
			//feSkillJigi = true;
			//currentSkill = new SkillFe();
		}

	}

	public void SetCurrentSkill(SkillBase skillBase)
	{
		if (currentSkill != null)
		{
			currentSkill.ResetSkill(this);
		}

		currentSkill = skillBase;
	}

	private void ExecuteSkill()
	{
		if(Input.GetKeyDown(KeyCode.Z))
		{
			Debug.Log("ExecuteSkill");
			if (currentSkill != null)
			{
				currentSkill.Execute(this);
			}
		}
	}

	public void ResetSkill()
	{
		currentSkill?.ResetSkill(this);
		currentSkill = null;
	}

	/// <summary>
	/// 死亡.
	/// </summary>
	private void Death()
	{
		// 死亡処理の実装があれば追加
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	protected void OnDrawGizmos()
	{
		Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
	}
}