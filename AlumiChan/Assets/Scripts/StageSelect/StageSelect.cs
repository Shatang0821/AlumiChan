using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン遷移に必要

public class StageSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	// ゲーム開始ボタンが押されたときに呼ばれるメソッド
	public void stage1()
	{
		// "GameScene"というシーンに遷移する
		SceneManager.LoadScene("stage");
	}
	public void stage2()
	{
		// "GameScene"というシーンに遷移する
		SceneManager.LoadScene("stage2");
	}
	public void stage3()
	{
		// "GameScene"というシーンに遷移する
		SceneManager.LoadScene("stage3");
	}
	public void stage4()
	{
		// "GameScene"というシーンに遷移する
		SceneManager.LoadScene("stage4");
	}

}
