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
	private float knockbackForce = 10.0f; // Knockback�̋���
	[SerializeField]
	[Header("�m�b�N�o�b�N�̂̎�������")]
	private float knockbackDuration = 0.2f;// Knockback�̎������ԁu
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
	public void Move()
	{
		// �L�[���͂𒼐ڃ`�F�b�N.
		float x = 0;
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

	/// <summary>
	/// �W�����v.
	/// </summary>
	public void Jump()
	{
		// �W�����v����: �n�ʂɐڂ��Ă���ꍇ�̂�.
		if (Input.GetKeyDown(KeyCode.Space) && !Jumping)
		{
			// �W�����v�͂�������.
			rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
			Jumping = true;
		}
	}

	private void Knockback(Vector2 knockbackDuration)
	{
		//�m�b�N�o�b�N�̒���ݒ�.
		KnockedBack = true;

		//�m�b�N�o�b�N�̗͂�������.
		rigidbody2D.AddForce(knockbackDuration * knockbackForce, ForceMode2D.Impulse);

		//�m�b�N�o�b�N�̎������Ԍ�Ƀt���O��߂�.
	
	}
	private IEnumerator ResetKnockback()
	{
		yield return new WaitForSeconds(knockbackDuration);
		KnockedBack = false;
	}



	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag(groundTag))
		{
			// �n�ʂƐڐG������W�����v�\�ɖ߂�.
			Jumping = false;
		}

		if (collision.gameObject.CompareTag("enemy"))
		{
			Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
			Knockback(knockbackDirection);
		}

	}



}
