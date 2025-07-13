using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _scoreCanvas;
    [SerializeField] private TextMeshProUGUI _inGameScoreText;
    [SerializeField] private TextMeshProUGUI _finalScoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TextMeshProUGUI _finalScoreTitleText;
    [SerializeField] private TextMeshProUGUI _highScoreTileText;

    private int currentScore = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _inGameScoreText.text = currentScore.ToString();
        _highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        UpdateHighScore();
        _scoreCanvas.SetActive(false);
        ChangeBackground();
    }
    public void UpdateHighScore()
    {
        if (currentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            _highScoreText.text = currentScore.ToString();
        }
    }
    public void UpdateScore()
    {
        currentScore++;
        _inGameScoreText.text = currentScore.ToString();
        _finalScoreText.text = currentScore.ToString();
        UpdateHighScore();
    }
    public void ShowCurrentScore()
    {
        _scoreCanvas.SetActive(true);
        _inGameScoreText.gameObject.SetActive(true);
        _finalScoreTitleText.gameObject.SetActive(false);
        _highScoreTileText.gameObject.SetActive(false);
        _finalScoreText.gameObject.SetActive(false);
        _highScoreText.gameObject.SetActive(false);
        Debug.Log("Show Current Score...");
    }
    public void ShowFinalScore()
    {
        _inGameScoreText.gameObject.SetActive(false);
        _finalScoreTitleText.gameObject.SetActive(true);
        _highScoreTileText.gameObject.SetActive(true);
        _finalScoreText.gameObject.SetActive(true);
        _highScoreText.gameObject.SetActive(true);
        Debug.Log("Show Final Score...");
    }
    public void UpdateScoreBoard()
    {
        int firstScore = PlayerPrefs.GetInt("FirstScore");
        int secondScore = PlayerPrefs.GetInt("SecondScore");
        int thirdScore = PlayerPrefs.GetInt("ThirdScore");

        if (currentScore > firstScore)
        {
            PlayerPrefs.SetInt("ThirdScore", secondScore);
            PlayerPrefs.SetInt("SecondScore", firstScore);
            PlayerPrefs.SetInt("FirstScore", currentScore);
        }
        else if (currentScore > secondScore)
        {
            PlayerPrefs.SetInt("ThirdScore", secondScore);
            PlayerPrefs.SetInt("SecondScore", currentScore);
        }
        else if (currentScore > thirdScore)
        {
            PlayerPrefs.SetInt("ThirdScore", currentScore);
        }
    }
    public void ChangeBackground()
    {
        string newBackgroundName = PlayerPrefs.GetString("CurrentBackground");
        string backgroundPath = "Backgrounds/" + newBackgroundName;
        if (backgroundPath != null)
        {
            _background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(backgroundPath);
            Debug.Log("Set Background To: " + newBackgroundName + " Changed Background Successfully!");
        }
        else
        {
            Debug.LogWarning("Not Found Background Path At: " + backgroundPath);
        }
    }
}
