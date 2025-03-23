using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���J�ڂɕK�v.
using UnityEngine.UI; // UI�R���|�[�l���g�ւ̃A�N�Z�X�ɕK�v.

public class ClearManager : MonoBehaviour
{
	//�N���b�N���b�Z�[�W��\������.
	public Text clickText;

	//�N���b�N���̂��߂�AddSource.
	public AudioSource clickAudio;

	// AudioClip�𒼐ڃA�^�b�`�ł���悤�ɂ���.
	public AudioClip clickSound;
	// Start is called before the first frame update
	void Start()
    {
		// �e�L�X�g�����蓖�Ă��Ă���ꍇ�A������Ԃł͔�\���ɂ���
		if (clickText != null)
		{
			clickText.gameObject.SetActive(false);
		}

		// AudioSource���A�^�b�`����Ă��Ȃ��ꍇ�͒ǉ�����
		if (clickAudio == null)
		{
			clickAudio = gameObject.AddComponent<AudioSource>();
		}

		// AudioClip���ݒ肳��Ă���ꍇ��AudioSource�Ɋ��蓖�Ă�
		if (clickSound != null && clickAudio != null)
		{
			clickAudio.clip = clickSound;
		}


	}

	// Update is called once per frame
	void Update()
    {
        
    }
	// �{�^���������ꂽ�Ƃ��ɌĂяo�����֐�
	public void ShowClickMessage()
	{
		// �N���b�N�����Đ�
		PlayClickSound();

		if (clickText != null)
		{
			// �e�L�X�g��\��
			clickText.text = "Click On";
			clickText.gameObject.SetActive(true);

			// 1�b��Ƀe�L�X�g���\���ɂ���
			StartCoroutine(HideClickTextAfterDelay(1.0f));
		}
	}

	// �N���b�N�����Đ�����֐�
	public void PlayClickSound()
	{
		if (clickAudio != null)
		{
			clickAudio.Play();
		}
	}

	// ��莞�Ԍ�ɃN���b�N�e�L�X�g���\���ɂ���R���[�`��
	private IEnumerator HideClickTextAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		if (clickText != null)
		{
			clickText.gameObject.SetActive(false);
		}
	}

	public void Select()
	{
		// �{�^���N���b�N���b�Z�[�W��\���i�����炷�j
		ShowClickMessage();

		// �����x�����Ă���V�[���J�ځi�N���b�N���b�Z�[�W�������邽�߁j
		StartCoroutine(LoadSceneWithDelay("StageSelect", 0.5f));
	}

	public void Title()
	{
		// �{�^���N���b�N���b�Z�[�W��\���i�����炷�j
		ShowClickMessage();

		// �����x�����Ă���V�[���J�ځi�N���b�N���b�Z�[�W�������邽�߁j
		StartCoroutine(LoadSceneWithDelay("Title", 0.5f));
	}

	// �x���t���ŃV�[�������[�h����R���[�`��
	private IEnumerator LoadSceneWithDelay(string sceneName, float delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(sceneName);
	}
}
