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
	//スキル実行.
	/// <summary>
	public void Excute()
	{
		if(controller != null)
		{
			controller.SetJumpPower();
		}
	}
}
