using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 2.0f;
    private string gameSceneName = "GameScene"; // Replace with your game scene name

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickExit()
    {
        StartCoroutine(FadeAndExitGame());
    }

    public void OnClickStart()
    {
        StartCoroutine(FadeAndStartGame());
    }

    private IEnumerator FadeAndStartGame()
    {
        float currentTime = 0;
        Color originalColor = fadePanel.color;

        while (currentTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, currentTime / fadeDuration);
            fadePanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        fadePanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
        SceneManager.LoadScene(gameSceneName);
    }

    private IEnumerator FadeAndExitGame()
    {
        float currentTime = 0;
        Color originalColor = fadePanel.color;

        while (currentTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, currentTime / fadeDuration);
            fadePanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        fadePanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
        Application.Quit();
    }
}