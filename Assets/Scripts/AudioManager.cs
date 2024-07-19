using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> clipList;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (clipList.Count > 0)
        {
            audioSource.clip = clipList[0];
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < clipList.Count)
        {
            audioSource.PlayOneShot(clipList[index]);
        }
    }
}
