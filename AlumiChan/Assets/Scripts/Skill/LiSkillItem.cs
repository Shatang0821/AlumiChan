using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiSkillItem : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//íœ.
		if (collision.gameObject.CompareTag("Player"))
		{
			//ƒAƒCƒeƒ€æ‚Á‚½ˆ—
			collision.gameObject.GetComponent<PlayerController>().SetSkill("Li");
			Destroy(this.gameObject);
		}
	}
}
