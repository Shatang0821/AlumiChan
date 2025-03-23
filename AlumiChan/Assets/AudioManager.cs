using System.Collections;
using System.Collections.Generic;
using FrameWork.Utils;
using UnityEngine;

public class AudioManager : UnityPersistentSingleton<AudioManager>
{
    [SerializeField] AudioSource sFXPlayer;
    [SerializeField] AudioSource loopSFXPlayer; // ���[�v�Đ��p
    [SerializeField] float minPitch = 0.9f;

    [SerializeField] float maxPitch = 1.1f;

    /// <summary>
    /// �����o��
    /// </summary>
    /// <param name="audioData">���f�[�^</param>
    public void PlaySFX(AudioData audioData)
    {
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.volueme);
    }

    /// <summary>
    /// Pitch�������_���ɕύX���ĉ����o��
    /// </summary>
    /// <param name="audioData">���f�[�^</param>
    public void PlayRandomSFX(AudioData audioData)
    {
        sFXPlayer.pitch = Random.Range(minPitch, maxPitch);
        PlaySFX(audioData);
    }

    /// <summary>
    /// �������̉����������_���ɗ���
    /// </summary>
    /// <param name="audioData">���f�[�^�z��</param>
    public void PlayRandomSFX(AudioData[] audioData)
    {
        PlayRandomSFX(audioData[Random.Range(0, audioData.Length)]);
    }
    
    /// <summary>
    /// �����Ȃǂ̃��[�vSE���Đ�����
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
    /// ���[�vSE���~�߂�
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
/// AudioClip��volueme���܂Ƃ߂�N���X
/// </summary>
[System.Serializable]
public class AudioData
{
    /// <summary>
    /// ����
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// ����
    /// </summary>
    public float volueme;
}
