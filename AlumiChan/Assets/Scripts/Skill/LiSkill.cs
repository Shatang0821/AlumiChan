using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiSkill : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//çÌèú.
		if (collision.gameObject.CompareTag("Player"))
		{
			Destroy(this.gameObject);
		}
	}
}
