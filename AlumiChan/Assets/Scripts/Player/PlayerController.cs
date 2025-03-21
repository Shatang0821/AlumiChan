using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	/*============�֐��Ǘ�==========*/
	[SerializeField]
	private float moveSpeed = 5f;
	//private Animator anim = null;
	// Start is called before the first frame update
	/*=============================*/
	void Start()
    {
		//anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		Move();

	}

	/// <summary>
	/// �v���C���[�ړ�.
	/// </summary>
	public void Move()
	{
		// ���������̓��͂��擾�iA/D�L�[�܂���Left/Right���L�[�j
		float x = Input.GetAxisRaw("Horizontal");

		if (x != 0)
		{
			// �ړ������ɉ����ăL�����N�^�[�̌�����ύX.
			transform.localScale = new Vector3(x * 1, 1, 1);
			// �ړ�����
			transform.position += new Vector3(x * moveSpeed * Time.deltaTime, 0, 0);
			// �A�j���[�V�����ݒ�
			//anim.SetBool("Move", true);
		}
		else
		{
			// �ړ����Ă��Ȃ����̓A�j���[�V�������~
			//anim.SetBool("Move", false);
		}
	}
	/// <summary>
	/// �W�����v.
	/// </summary>
	public void Jump()
	{

	}
	/// <summary>
	/// ���S.
	/// </summary>
	public void Death()
	{

	}
	/// <summary>
	/// �A�C�e��.
	/// </summary>
	public void Item()
	{

	}
}

