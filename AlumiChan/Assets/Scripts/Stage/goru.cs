using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���Ǘ��@�\���g�p���邽�߂ɒǉ�

public class goru : MonoBehaviour
{
	// �����[�h�p�̃L�[�ݒ�
	[SerializeField] private KeyCode reloadKey = KeyCode.O; // �f�t�H���g��R�L�[��ݒ�

	// �Q�[���J�n���Ɉ�x�������s�����
	void Start()
	{

	}

	// �t���[�����ƂɎ��s�����
	void Update()
	{
		// �w�肳�ꂽ�L�[�������ꂽ��V�[���������[�h
		if (Input.GetKeyDown(reloadKey))
		{
			// ���݂̃V�[�����ēǂݍ���
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	// 2D�R���C�_�[�����̃R���C�_�[�ɏՓ˂����Ƃ��ɌĂяo�����
	void OnCollisionEnter2D(Collision2D collision)
	{
		// �Փ˂����I�u�W�F�N�g��"player"�^�O�������Ă��邩�m�F
		if (collision.gameObject.CompareTag("Player"))
		{
			// "clear"�V�[�������[�h
			SceneManager.LoadScene("Clear");
		}
	}
}