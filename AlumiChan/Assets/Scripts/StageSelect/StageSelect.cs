using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���J�ڂɕK�v

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
	// �Q�[���J�n�{�^���������ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
	public void stage1()
	{
		// "GameScene"�Ƃ����V�[���ɑJ�ڂ���
		SceneManager.LoadScene("stage");
	}
	public void stage2()
	{
		// "GameScene"�Ƃ����V�[���ɑJ�ڂ���
		SceneManager.LoadScene("stage2");
	}
	public void stage3()
	{
		// "GameScene"�Ƃ����V�[���ɑJ�ڂ���
		SceneManager.LoadScene("stage3");
	}
	public void stage4()
	{
		// "GameScene"�Ƃ����V�[���ɑJ�ڂ���
		SceneManager.LoadScene("stage4");
	}

}
