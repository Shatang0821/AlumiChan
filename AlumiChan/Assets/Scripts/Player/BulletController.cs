using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	[SerializeField]
	[Header("�e�̑��x")]
	private float speed = 10f;         // �e�̑��x
	private float direction = 1f;       // �e�̕��� (1: �E, -1: ��)
	[SerializeField]
	[Header("�e�̕\������")]
	private float lifetime = 3f;        // �e�̐�������
	[SerializeField]
	[Header("�G�l�~�[�̃^�O")]
	private string enemyTag = "enemy";  // �G�̃^�O

	void Start()
	{
		// �w�肳�ꂽ�b����ɒe��j��
		Destroy(gameObject, lifetime);
	}

	void Update()
	{
		// �e������ɉ����Ĉړ�
		transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
	}

	// �e�̑��x��ݒ�
	public void SetSpeed(float newSpeed)
	{
		speed = newSpeed;
	}

	// �e�̕�����ݒ�
	public void SetDirection(float newDirection)
	{
		direction = newDirection;

		// �����ɉ����Ēe�̃X�v���C�g�𔽓]
		if (direction < 0)
		{
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}
	}

	// �Փ˔���
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �G�ɓ��������ꍇ
		if (collision.CompareTag(enemyTag))
		{
			// �e��j��
			collision.gameObject.GetComponent<EnemyMove>().TakeDamage();
			Destroy(gameObject);
		}
		// �n�ʂ�ǂɓ��������ꍇ
		else if (collision.CompareTag("Environment"))
		{
			// �e��j��
			Destroy(gameObject);
		}
	}
}