using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    private int DifficultySetting;
    public Dropdown DifficultyDropdown;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeDifficulty()
    {
        DifficultySetting = DifficultyDropdown.value;
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            GameManager.Difficulty = DifficultySetting;
            Debug.Log("Difficulty: " + DifficultySetting);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
