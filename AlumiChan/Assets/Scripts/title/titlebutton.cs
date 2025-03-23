using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン遷移に必要

public class titlebutton : MonoBehaviour
{
	// 音声用のAudioSourceコンポーネント
	private AudioSource audioSource;

	// クリック時の効果音
	[SerializeField] private AudioClip clickSound;
	// Start is called before the first frame update
	void Start()
    {
		// AudioSourceコンポーネントを取得
		audioSource = GetComponent<AudioSource>();

		// AudioSourceがアタッチされていない場合は追加
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	// 効果音を再生してからシーンを読み込む関数
	private IEnumerator PlaySoundAndLoadScene(string sceneName)
	{
		// 効果音を再生
		audioSource.PlayOneShot(clickSound);

		// 効果音の再生が終わるまで少し待つ（短い効果音なら0.1〜0.3秒程度）
		yield return new WaitForSeconds(0.2f);

		// シーンを読み込む
		SceneManager.LoadScene(sceneName);
	}
	// ゲーム開始ボタンが押されたときに呼ばれるメソッド
	public void StartGame()
	{
		// セレクト画面に移動.
		StartCoroutine(PlaySoundAndLoadScene("StageSelect"));
	}
}
