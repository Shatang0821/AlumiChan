using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiSkillItem : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//削除.
		if (collision.gameObject.CompareTag("Player"))
		{
			//アイテム取った処理
			collision.gameObject.GetComponent<PlayerController>().SetSkill("Li");
			Destroy(this.gameObject);
		}
	}
}
