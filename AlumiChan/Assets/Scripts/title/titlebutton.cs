using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���J�ڂɕK�v

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
	// �Q�[���J�n�{�^���������ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
	public void StartGame()
	{
		// "GameScene"�Ƃ����V�[���ɑJ�ڂ���
		SceneManager.LoadScene("StageSelect");
	}
}
