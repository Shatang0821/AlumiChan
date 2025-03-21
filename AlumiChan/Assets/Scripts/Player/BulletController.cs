using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	[SerializeField]
	[Header("弾の速度")]
	private float speed = 10f;         // 弾の速度
	private float direction = 1f;       // 弾の方向 (1: 右, -1: 左)
	[SerializeField]
	[Header("弾の表示時間")]
	private float lifetime = 3f;        // 弾の生存時間
	[SerializeField]
	[Header("エネミーのタグ")]
	private string enemyTag = "enemy";  // 敵のタグ

	void Start()
	{
		// 指定された秒数後に弾を破壊
		Destroy(gameObject, lifetime);
	}

	void Update()
	{
		// 弾を方向に応じて移動
		transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
	}

	// 弾の速度を設定
	public void SetSpeed(float newSpeed)
	{
		speed = newSpeed;
	}

	// 弾の方向を設定
	public void SetDirection(float newDirection)
	{
		direction = newDirection;

		// 方向に応じて弾のスプライトを反転
		if (direction < 0)
		{
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}
	}

	// 衝突判定
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 敵に当たった場合
		if (collision.CompareTag(enemyTag))
		{
			// 弾を破壊
			Destroy(gameObject);
		}
		// 地面や壁に当たった場合
		else if (collision.CompareTag("Ground") || collision.CompareTag("Wall"))
		{
			// 弾を破壊
			Destroy(gameObject);
		}
	}
}