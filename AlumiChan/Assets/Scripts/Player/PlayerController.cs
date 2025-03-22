using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	private Vector2 knockbackForce = new Vector2(10.0f,0); // Knockback�̋���
	[SerializeField]
	[Header("�m�b�N�o�b�N�̂̎�������")]
	private float knockbackDuration = 0.2f;// Knockback�̎�������
	/*==============================*/

	/*============�n�ʕϐ��Ǘ�==========*/
	[Header("�n�ʂ̃^�O��")]
	private string groundTag = "Ground"; // �n�ʂ̃^�O��.
	[SerializeField]
	[Header("�n�ʂɐڂ��Ă��邩�ǂ���")]
	/*==============================*/

	private bool Ground = false; // �n�ʂɐڂ��Ă��邩�ǂ���.
	private bool Jumping = false; // �W�����v�����ǂ�����ǐՂ���t���O.
	private bool KnockedBack = false; // �m�b�N�o�b�N�����ǂ������Ǘ�.

	void Start()
	{
		// Rigidbody2D�R���|�[�l���g���擾.
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		// �n�ʔ�����X�V.
		//CheckGrounded();

		// �v���C���[�̈ړ��ƃW�����v.
		Move();
		Jump();
	}

	/// <summary>
	/// �v���C���[�ړ�.
	/// </summary>
	private void Move()
	{
		// �L�[���͂𒼐ڃ`�F�b�N.
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
			// �ړ������ɉ����ăL�����N�^�[�̌�����ύX.
			transform.localScale = new Vector3(x * 1, 1, 1);
			// �ړ����� - moveSpeed���g�p.
			transform.position += new Vector3(x * moveSpeed * Time.deltaTime, 0, 0);
		   }
	    }
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
		}
	}
	
	/// <summary>
	/// �W�����v�͊Ǘ�
	/// </summary>
	private void SetJump()
	{

	}
	

	private void Knockback(Vector2 knockbackDuration)
	{
		rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
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
		if (collision.gameObject.CompareTag(groundTag))
		{
			// �n�ʂƐڐG������W�����v�\�ɖ߂�.
			Jumping = false;
		}
		//�G�l�~�[�ɓ���������m�b�N�o�b�N����.
		if (collision.gameObject.CompareTag("enemy"))
		{
			Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
			Knockback(knockbackDirection);
		}

		//���S.
		if(collision.gameObject.CompareTag("desu"))
		{
			Destroy(this.gameObject);
		}

	}

	/// <summary>
	/// ���S.
	/// </summary>
	private void Death()
	{

	}

	/// <summary>
	/// �A�C�e��.
	/// </summary>
	public void Item()
	{

	}

}
