using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreenHandler : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 2.0f;
    private string gameSceneName = "GameScene"; // Replace with your game scene name
    private string mainMenuName = "MainMenu"; // Replace with your game scene name
    public void OnClickRestart()
    {
        StartCoroutine(FadeAndRestart());
    }

    public void OnClickMainMenu()
    {
        StartCoroutine(FadeAndMainMenu());
    }

    private IEnumerator FadeAndMainMenu()
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
        SceneManager.LoadScene(mainMenuName);
    }

    private IEnumerator FadeAndRestart()
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
}
