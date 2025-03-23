using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiSkillItem : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//�폜.
		if (collision.gameObject.CompareTag("Player"))
		{
			//�A�C�e�����������
			collision.gameObject.GetComponent<PlayerController>().SetSkill("Li");
			Destroy(this.gameObject);
		}
	}
}
