using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	/*============関数管理==========*/
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
	/// プレイヤー移動.
	/// </summary>
	public void Move()
	{
		// 水平方向の入力を取得（A/DキーまたはLeft/Right矢印キー）
		float x = Input.GetAxisRaw("Horizontal");

		if (x != 0)
		{
			// 移動方向に応じてキャラクターの向きを変更.
			transform.localScale = new Vector3(x * 1, 1, 1);
			// 移動処理
			transform.position += new Vector3(x * moveSpeed * Time.deltaTime, 0, 0);
			// アニメーション設定
			//anim.SetBool("Move", true);
		}
		else
		{
			// 移動していない時はアニメーションを停止
			//anim.SetBool("Move", false);
		}
	}
	/// <summary>
	/// ジャンプ.
	/// </summary>
	public void Jump()
	{

	}
	/// <summary>
	/// 死亡.
	/// </summary>
	public void Death()
	{

	}
	/// <summary>
	/// アイテム.
	/// </summary>
	public void Item()
	{

	}
}

