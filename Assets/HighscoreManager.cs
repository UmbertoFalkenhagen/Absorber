using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager Instance { get; private set; }

    public int highScore = 0;
    public TextMeshProUGUI highScoreText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    private void Update()
    {
        
            GameObject highScoreTextObject = GameObject.FindGameObjectWithTag("HighscoreText");
            if (highScoreTextObject != null)
            {
                highScoreText = highScoreTextObject.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogError("HighscoreText object not found. Make sure it's tagged correctly.");
            }
        
        
        UpdateHighScoreText();
    }

    public void SetHighScore(int score)
    {
        highScore += score;
        UpdateHighScoreText();
    }

    void UpdateHighScoreText()
    {
        // Update the TMP text element to display the high score
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}
