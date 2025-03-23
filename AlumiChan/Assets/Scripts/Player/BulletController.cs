using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	[SerializeField]
	[Header("’e‚Ì‘¬“x")]
	private float speed = 10f;         // ’e‚Ì‘¬“x
	private float direction = 1f;       // ’e‚Ì•ûŒü (1: ‰E, -1: ¶)
	[SerializeField]
	[Header("’e‚Ì•\¦ŠÔ")]
	private float lifetime = 3f;        // ’e‚Ì¶‘¶ŠÔ
	[SerializeField]
	[Header("ƒGƒlƒ~[‚Ìƒ^ƒO")]
	private string enemyTag = "enemy";  // “G‚Ìƒ^ƒO

	void Start()
	{
		// w’è‚³‚ê‚½•b”Œã‚É’e‚ğ”j‰ó
		Destroy(gameObject, lifetime);
	}

	void Update()
	{
		// ’e‚ğ•ûŒü‚É‰‚¶‚ÄˆÚ“®
		transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
	}

	// ’e‚Ì‘¬“x‚ğİ’è
	public void SetSpeed(float newSpeed)
	{
		speed = newSpeed;
	}

	// ’e‚Ì•ûŒü‚ğİ’è
	public void SetDirection(float newDirection)
	{
		direction = newDirection;

		// •ûŒü‚É‰‚¶‚Ä’e‚ÌƒXƒvƒ‰ƒCƒg‚ğ”½“]
		if (direction < 0)
		{
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}
	}

	// Õ“Ë”»’è
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// “G‚É“–‚½‚Á‚½ê‡
		if (collision.CompareTag(enemyTag))
		{
			// ’e‚ğ”j‰ó
			collision.gameObject.GetComponent<EnemyMove>().TakeDamage();
			Destroy(gameObject);
		}
		// ’n–Ê‚â•Ç‚É“–‚½‚Á‚½ê‡
		else if (collision.CompareTag("Environment"))
		{
			// ’e‚ğ”j‰ó
			Destroy(gameObject);
		}
	}
}