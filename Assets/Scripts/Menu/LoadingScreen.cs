using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

	public Image loadingScreen;


	void OnLevelWasLoaded(int level)
	{
		if (level == 1) 
		{
			StartCoroutine ("ScreenAwake");
		}
	}

    public IEnumerator ScreenSleep()
    {
        loadingScreen.enabled = true;

        byte alpha = 255;
        float step = 255.0f / 60.0f;

        for (int time = 0; time < 60; time++)
        {
            yield return 0;
            alpha += (byte)step;
            loadingScreen.color = new Color32(255, 255, 255, alpha);
        }

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            SceneManager.LoadSceneAsync(0);
            Destroy(this.gameObject);
        }
        else
        {
            SceneManager.LoadSceneAsync(1);
            
        }

    }

	private IEnumerator ScreenAwake()
	{
		byte alpha = 255;
		float step = 255.0f / 60.0f;

		for (int time = 0; time < 60; time++) 
		{
			yield return 0;
			alpha -= (byte)step;
			loadingScreen.color = new Color32 (255, 255, 255, alpha);
		}

        loadingScreen.enabled = false;
	}
}
