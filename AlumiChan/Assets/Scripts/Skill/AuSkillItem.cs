using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuSkillItem : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//
		if (collision.gameObject.CompareTag("Player"))
		{
			//アイテム取った処理
			collision.gameObject.GetComponent<PlayerController>().SetSkill("Au");
			
			Destroy(this.gameObject);
		}
	}
}
