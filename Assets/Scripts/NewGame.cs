using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Image title;
    
    public void LoadNewGame()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync ()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        
        title.gameObject.SetActive(false);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }
    }
}
