using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> clips = new List<AudioClip>();
    public List<AudioClip> voiceClips = new List<AudioClip>();

    public void ButtonClick()
    {
        audioSource.clip = clips[1];
        audioSource.Play();
    }

    public void GameFail()
    {
        audioSource.clip = clips[2];
        audioSource.Play();
    }

    public void GameClear()
    {
        audioSource.clip = clips[0];
        audioSource.Play();

    }

    public void PlayVoice_00()
    {
        audioSource.clip = voiceClips[0];
        audioSource.Play();
    }
    public void PlayVoice_01()
    {
        audioSource.clip = voiceClips[0];
        audioSource.Play();
    }

    public void PlayVoice_02()
    {
        audioSource.clip = voiceClips[1];
        audioSource.Play();
    }
    public void PlayVoice_03()
    {
        audioSource.clip = voiceClips[2];
        audioSource.Play();
    }
    public void PlayVoice_04()
    {
        audioSource.clip = voiceClips[3];
        audioSource.Play();
    }
    public void PlayVoice_05()
    {
        audioSource.clip = voiceClips[4];
        audioSource.Play();
    }
    public void PlayVoice_06()
    {
        audioSource.clip = voiceClips[5];
        audioSource.Play();
    }
    public void PlayVoice_07()
    {
        audioSource.clip = voiceClips[6];
        audioSource.Play();
    }
    public void PlayVoice_08()
    {
        audioSource.clip = voiceClips[7];
        audioSource.Play();
    }
    public void PlayVoice_10()
    {
        audioSource.clip = voiceClips[8];
        audioSource.Play();
    }
    public void PlayVoice_11()
    {
        audioSource.clip = voiceClips[9];
        audioSource.Play();
    }
    public void PlayVoice_12()
    {
        audioSource.clip = voiceClips[10];
        audioSource.Play();
    }
    public void PlayVoice_Fail()
    {
        audioSource.clip = voiceClips[11];
        audioSource.Play();
    }
    public void PlayVoice_Clear()
    {
        audioSource.clip = voiceClips[12];
        audioSource.Play();

    }

}
