using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン遷移に必要

public class titlebutton : MonoBehaviour
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
	public void StartGame()
	{
		// "GameScene"というシーンに遷移する
		SceneManager.LoadScene("StageSelect");
	}
}
