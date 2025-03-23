using System.Collections;
using System.Collections.Generic;
using FrameWork.Utils;
using UnityEngine;

public class AudioManager : UnityPersistentSingleton<AudioManager>
{
    [SerializeField] AudioSource sFXPlayer;
    [SerializeField] AudioSource loopSFXPlayer; // ループ再生用
    [SerializeField] float minPitch = 0.9f;

    [SerializeField] float maxPitch = 1.1f;

    /// <summary>
    /// 音を出す
    /// </summary>
    /// <param name="audioData">音データ</param>
    public void PlaySFX(AudioData audioData)
    {
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.volueme);
    }

    /// <summary>
    /// Pitchをランダムに変更して音を出す
    /// </summary>
    /// <param name="audioData">音データ</param>
    public void PlayRandomSFX(AudioData audioData)
    {
        sFXPlayer.pitch = Random.Range(minPitch, maxPitch);
        PlaySFX(audioData);
    }

    /// <summary>
    /// いくつかの音源をランダムに流す
    /// </summary>
    /// <param name="audioData">音データ配列</param>
    public void PlayRandomSFX(AudioData[] audioData)
    {
        PlayRandomSFX(audioData[Random.Range(0, audioData.Length)]);
    }
    
    /// <summary>
    /// 足音などのループSEを再生する
    /// </summary>
    public void PlayLoopSFX(AudioData audioData)
    {
        if (loopSFXPlayer.isPlaying) return;

        loopSFXPlayer.clip = audioData.audioClip;
        loopSFXPlayer.volume = audioData.volueme;
        loopSFXPlayer.loop = true;
        loopSFXPlayer.Play();
    }

    /// <summary>
    /// ループSEを止める
    /// </summary>
    public void StopLoopSFX()
    {
        if (loopSFXPlayer.isPlaying)
        {
            loopSFXPlayer.Stop();
            loopSFXPlayer.clip = null;
        }
    }
}

/// <summary>
/// AudioClipとvoluemeをまとめるクラス
/// </summary>
[System.Serializable]
public class AudioData
{
    /// <summary>
    /// 音源
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// 音量
    /// </summary>
    public float volueme;
}
