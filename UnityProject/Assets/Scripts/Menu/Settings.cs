using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour {
    //Used to apply settings and carry them to the gameplay level

    [Header("Settings objects:")]
    public static bool SoundOn = true;
    public Dropdown DifficultyDropdown;

    private int DifficultySetting;

    [Header("Audio:")]
    public AudioClip ingameNoAction, ingameAction;
    public bool immediate = false;
    public AudioMixer soundMixer;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //Sets difficulty to dropdown value
    public void ChangeDifficulty()
    {
        DifficultySetting = DifficultyDropdown.value;
    }

    //Toggles audio
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
            //Applies difficulty from main menu to gameplay level 
            GameManager.Difficulty = DifficultySetting;
            Debug.Log("Difficulty: " + DifficultySetting);

            //Starts in game music with volume fade
            immediate = false;
            StartCoroutine("switchClip",ingameNoAction);
        }
    }

    //Used to change music clip thats currently playing
    public IEnumerator switchClip(AudioClip switchTo)
    {
        AudioSource a = GetComponent<AudioSource>();

        //Whether it should be changed immediatley or use the volume fade
        if (immediate)
        {
            a.clip = switchTo;
            a.Play();
            yield break;
        }

        //Volume down
        for(float timer = a.volume; timer>0; timer-=0.1f)
        {
            yield return new WaitForSeconds(0.2f);
            a.volume = timer;
        }

        a.clip = switchTo;
        a.Play();

        //Volume up
        for (float timer2 = a.volume; timer2 < 1; timer2 += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            a.volume = timer2;
        }

    }
}
