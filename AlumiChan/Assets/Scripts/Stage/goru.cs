using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理機能を使用するために追加

public class goru : MonoBehaviour
{
	// リロード用のキー設定
	[SerializeField] private KeyCode reloadKey = KeyCode.O; // デフォルトでRキーを設定

	// ゲーム開始時に一度だけ実行される
	void Start()
	{

	}

	// フレームごとに実行される
	void Update()
	{
		// 指定されたキーが押されたらシーンをリロード
		if (Input.GetKeyDown(reloadKey))
		{
			// 現在のシーンを再読み込み
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	// 2Dコライダーが他のコライダーに衝突したときに呼び出される
	void OnCollisionEnter2D(Collision2D collision)
	{
		// 衝突したオブジェクトが"player"タグを持っているか確認
		if (collision.gameObject.CompareTag("Player"))
		{
			// "clear"シーンをロード
			SceneManager.LoadScene("Clear");
		}
	}
}