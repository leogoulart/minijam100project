using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LvlManager : MonoBehaviour
{

    public GameObject loading;
    public Slider slider;
    public Text progressText;

    public void LoadLevel (int sceneNumber)
    {
        StartCoroutine(startGame(sceneNumber));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator startGame (int sceneNumber)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNumber);
        loading.SetActive (true);

        while(!operation.isDone)
        {
            float progresso = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progresso;
            progressText.text = progresso * 100f + " %";

            yield return null;
        }
    }
    
    

    
    
}
