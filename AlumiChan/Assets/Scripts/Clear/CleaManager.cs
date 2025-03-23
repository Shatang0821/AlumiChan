using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン遷移に必要.
using UnityEngine.UI; // UIコンポーネントへのアクセスに必要.

public class ClearManager : MonoBehaviour
{
	//クリックメッセージを表示する.
	public Text clickText;

	//クリック音のためのAddSource.
	public AudioSource clickAudio;

	// AudioClipを直接アタッチできるようにする.
	public AudioClip clickSound;
	// Start is called before the first frame update
	void Start()
    {
		// テキストが割り当てられている場合、初期状態では非表示にする
		if (clickText != null)
		{
			clickText.gameObject.SetActive(false);
		}

		// AudioSourceがアタッチされていない場合は追加する
		if (clickAudio == null)
		{
			clickAudio = gameObject.AddComponent<AudioSource>();
		}

		// AudioClipが設定されている場合はAudioSourceに割り当てる
		if (clickSound != null && clickAudio != null)
		{
			clickAudio.clip = clickSound;
		}


	}

	// Update is called once per frame
	void Update()
    {
        
    }
	// ボタンが押されたときに呼び出される関数
	public void ShowClickMessage()
	{
		// クリック音を再生
		PlayClickSound();

		if (clickText != null)
		{
			// テキストを表示
			clickText.text = "Click On";
			clickText.gameObject.SetActive(true);

			// 1秒後にテキストを非表示にする
			StartCoroutine(HideClickTextAfterDelay(1.0f));
		}
	}

	// クリック音を再生する関数
	public void PlayClickSound()
	{
		if (clickAudio != null)
		{
			clickAudio.Play();
		}
	}

	// 一定時間後にクリックテキストを非表示にするコルーチン
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
		// ボタンクリックメッセージを表示（音も鳴らす）
		ShowClickMessage();

		// 少し遅延してからシーン遷移（クリックメッセージを見せるため）
		StartCoroutine(LoadSceneWithDelay("StageSelect", 0.5f));
	}

	public void Title()
	{
		// ボタンクリックメッセージを表示（音も鳴らす）
		ShowClickMessage();

		// 少し遅延してからシーン遷移（クリックメッセージを見せるため）
		StartCoroutine(LoadSceneWithDelay("Title", 0.5f));
	}

	// 遅延付きでシーンをロードするコルーチン
	private IEnumerator LoadSceneWithDelay(string sceneName, float delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(sceneName);
	}
}
