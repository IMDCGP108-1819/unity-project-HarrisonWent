using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour {

    private int DifficultySetting;
    public static bool SoundOn = true;
    public Dropdown DifficultyDropdown;
    public AudioClip ingameNoAction, ingameAction;
    public bool immediate = false;
    public AudioMixer soundMixer;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeDifficulty()
    {
        DifficultySetting = DifficultyDropdown.value;
    }

    public void ToggleAudio()
    {
        SoundOn = !SoundOn;
        if (SoundOn)
        {
            soundMixer.SetFloat("MasterVolume", 0);
        }
        else
        {
            soundMixer.SetFloat("MasterVolume", -80);
        }
        
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            GameManager.Difficulty = DifficultySetting;
            Debug.Log("Difficulty: " + DifficultySetting);
            immediate = false;
            StartCoroutine("switchClip",ingameNoAction);
        }
        else
        {
            
        }
    }

    public IEnumerator switchClip(AudioClip switchTo)
    {
        AudioSource a = GetComponent<AudioSource>();

        if (immediate)
        {
            a.clip = switchTo;
            a.Play();
            yield break;
        }

        for(float timer = a.volume; timer>0; timer-=0.1f)
        {
            yield return new WaitForSeconds(0.2f);
            a.volume = timer;
        }

        a.clip = switchTo;
        a.Play();

        for (float timer2 = a.volume; timer2 < 1; timer2 += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            a.volume = timer2;
        }

    }
}
