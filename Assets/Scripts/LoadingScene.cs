using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Text text;
    public Slider slider;
    public GameObject Panel;

    public void loadlevel(int LevelIndex)
    {
        StartCoroutine(LoadProgress(1));
        Panel.SetActive(true);
    }

    IEnumerator LoadProgress(int Level_index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(Level_index);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            int percentage = Mathf.RoundToInt(progress * 100); 
            text.text = percentage + "%";

            yield return null;
        }
    }
}
