using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rigidbody2D;

	/*============�ϐ��Ǘ�==========*/
	[SerializeField]
	[Header("�ړ��̑���")]
	private float moveSpeed = 5f;
	[SerializeField]
	[Header("�W�����v��")]
	private float jumpPower = 10.0f;
	[SerializeField]
	[Header("�m�b�N�o�b�N��")]
	private Vector2 knockbackForce = new Vector2(10.0f, 0); // Knockback�̋���
	[SerializeField]
	[Header("�m�b�N�o�b�N�̂̎�������")]
	private float knockbackDuration = 0.2f;// Knockback�̎�������
	/*==============================*/

	/*============�n�ʕϐ��Ǘ�==========*/
	// ���n�ʃ`�F�b�N
	[SerializeField] protected LayerMask whatIsGround; 
	[SerializeField] protected Transform groundCheck;         //�n�ʃ`�F�b�N
	[SerializeField] protected float groundCheckDistance;     //�`�F�b�N����//���C���[�ݒ�
	public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
	
	//[Header("�n�ʂ̃^�O��")]
	//private string groundTag = "Ground"; // �n�ʂ̃^�O��.
	//[SerializeField]
	//[Header("�n�ʂɐڂ��Ă��邩�ǂ���")]
	/*==============================*/

	/*============�d�C�e==========*/
	[Header("�e�̃I�u�W�F�N�g")]
	public GameObject AuBulletObj;//�e�̃I�u�W�F�N�g.
	[SerializeField]
	[Header("�e�̑��x")]
	private float bulletSpeed = 10f;//�e�̑��x.
	[Header("�e�̃N�[���^�C��(�b)")]
	[SerializeField]
	private float bulletCooldown = 1.5f; // �e�̃N�[���^�C��.
	private bool canShoot = true; // �e�����Ă邩�ǂ���.
	Vector3 bulletPoint;                // �e�̈ʒu.
	private float bulletDirection = 1f; // �e�̔��˕��� (1: �E, -1: ��).
	/*============================*/

	[SerializeField]
	private Animator _animator;
	[SerializeField]
	private Animator _nextAnimator;
	private bool Ground = false;      // �n�ʂɐڂ��Ă��邩�ǂ���.
	private bool Jumping = false;     // �W�����v�����ǂ�����ǐՂ���t���O.
	private bool KnockedBack = false; // �m�b�N�o�b�N�����ǂ������Ǘ�.

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
		// Rigidbody2D�R���|�[�l���g���擾.
		rigidbody2D = GetComponent<Rigidbody2D>();
		bulletPoint = transform.Find("BulletPoint").localPosition;
	}

	// Update is called once per frame
	void Update()
	{
		// �n�ʔ�����X�V.
		//CheckGrounded();

		// �v���C���[�̈ړ��ƃW�����v.
		Move();
		Jump();
		ExecuteSkill();
	}

	/// <summary>
	/// �v���C���[�ړ�.
	/// </summary>
	private void Move()
	{
		// �L�[���͂𒼐ڃ`�F�b�N.
		float x = 0;
		if (!KnockedBack)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				x = -1;
				bulletDirection = -1f; // �������ɐݒ�
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				x = 1;
				bulletDirection = 1f; // �E�����ɐݒ�
			}

			if (x != 0)
			{
				_animator.SetBool("Move",true);
				_nextAnimator.SetBool("Move",true);
				// �ړ������ɉ����ăL�����N�^�[�̌�����ύX.
				transform.localScale = new Vector3(x * 1, 1, 1);
				// �ړ����� - moveSpeed���g�p.
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
	/// �W�����v.
	/// </summary>
	private void Jump()
	{
		// �W�����v����: �n�ʂɐڂ��Ă���ꍇ�̂�.
		if (Input.GetKeyDown(KeyCode.Space) && !Jumping)
		{
			// �W�����v�͂�������.
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
	/// �W�����v�͊Ǘ�
	/// </summary>
	public void SetJumpPower()
	{
		jumpPower = 15;
	}
	/// <summary>
	/// ���`�E���X�L�����^�[��.
	/// </summary>
	public void ResetPower()
	{
		jumpPower = 12;
	}

	/// <summary>
	/// ���̃X�L��.
	/// </summary>
	public void electricity()
	{
		// �d�C���܂Ƃ��X�L���̎���������Βǉ�
	}


	public void InitElec()
	{
		KnockedBack = false;
		knockbackDuration = 0f;
	}
	/// <summary>
	/// �d�C���܂̊Ǘ�.
	/// </summary>
	public void electricityball()
	{
		
		// �N�[���^�C�����Ȃ猂�ĂȂ�
		if (!canShoot)
			return;

		// �e�𐶐����Ĕ��˕�����ݒ�
		Vector3 spawnPosition = transform.position + new Vector3(bulletPoint.x * bulletDirection, bulletPoint.y, bulletPoint.z);
		GameObject bullet = Instantiate(AuBulletObj, spawnPosition, Quaternion.identity);

		// BulletController���擾���Ēe�ɑ��x��ݒ�
		BulletController bulletController = bullet.GetComponent<BulletController>();
		if (bulletController != null)
		{
			bulletController.SetDirection(bulletDirection);
			bulletController.SetSpeed(bulletSpeed);
		}
		else
		{
			// BulletController���Ȃ��ꍇ��Rigidbody2D�𒼐ڑ���
			Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
			if (bulletRb != null)
			{
				bulletRb.velocity = new Vector2(bulletSpeed * bulletDirection, 0);
			}
		}

		// �N�[���^�C�����J�n
		StartCoroutine(BulletCooldown());
	}

	public void RestElec()
	{
		knockbackDuration = 0.2f;
	}
	
	
	/// <summary>
	/// �e�̃N�[���^�C������
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
		//�m�b�N�o�b�N�̒���ݒ�.
		KnockedBack = true;
		if (Jumping)
		{
			//�m�b�N�o�b�N�̗͂�������.
			rigidbody2D.AddForce(knockbackDuration * knockbackForce, ForceMode2D.Impulse);
		}
		else
		{
			rigidbody2D.AddForce(knockbackDuration * knockbackForce, ForceMode2D.Impulse);
		}

		//�m�b�N�o�b�N�̎������Ԍ�Ƀt���O��߂�.
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
			// �n�ʂƐڐG������W�����v�\�ɖ߂�.
			Jumping = false;
			_animator.SetBool("Jump",false);
		}
		//�G�l�~�[�ɓ���������m�b�N�o�b�N����.
		if (collision.gameObject.CompareTag("enemy"))
		{
			Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
			Knockback(knockbackDirection);
		}

		//���S.
		if (collision.gameObject.CompareTag("desu"))
		{
			Death();
		}

		//���`�E���X�L��.
		if (collision.gameObject.CompareTag("Li"))
		{
			//LiSkillJigi = true;
			//currentSkill = new SkillLi();
		}
		//�d�C���܂Ƃ�
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
	/// ���S.
	/// </summary>
	private void Death()
	{
		// ���S�����̎���������Βǉ�
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	protected void OnDrawGizmos()
	{
		Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
	}
}