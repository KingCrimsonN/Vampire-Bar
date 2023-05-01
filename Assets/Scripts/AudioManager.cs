using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

  public AudioSource musicSource;
  public List<AudioClip> musicClips;
  public List<AudioSource> sounds;
  public int index = -1;

  public void Play()
  {
    index++;
    musicSource.clip = musicClips[index];
    musicSource.Play(0);
  }

  public void PlaySound(int i)
  {
    sounds[i].Play();
  }

  public void Stop()
  {
    musicSource.Stop();
  }

}
