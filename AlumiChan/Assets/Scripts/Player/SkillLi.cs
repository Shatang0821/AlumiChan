using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLi : MonoBehaviour
{
	PlayerController controller;
	private void Start()
	{
		controller = GetComponent<PlayerController>();
	}

	/// <summary>
	//ÉXÉLÉãé¿çs.
	/// <summary>
	public void Excute()
	{
		if(controller != null)
		{
			controller.SetJumpPower();
		}
	}
}
