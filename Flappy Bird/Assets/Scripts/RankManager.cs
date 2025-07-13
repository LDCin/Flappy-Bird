using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankManager : MonoBehaviour
{
    public static RankManager instance;
    [SerializeField] private GameObject _background;
    [SerializeField] private TextMeshProUGUI _firstScoreText;
    [SerializeField] private TextMeshProUGUI _secondScoreText;
    [SerializeField] private TextMeshProUGUI _thirdScoreText;
    private int _firstScore;
    private int _secondScore;
    private int _thirdScore;

    void Awake()
    {
        ShowScoreBoard();
        ChangeBackground();
    }
    public void ShowScoreBoard()
    {
        _firstScore = PlayerPrefs.GetInt("FirstScore");
        _secondScore = PlayerPrefs.GetInt("SecondScore");
        _thirdScore = PlayerPrefs.GetInt("ThirdScore");
        _firstScoreText.text = _firstScore.ToString();
        _secondScoreText.text = _secondScore.ToString();
        _thirdScoreText.text = _thirdScore.ToString();
    }
    public void Return()
    {
        SceneManager.LoadScene("MenuGame");
    }
    public void RemoveScoreBoard()
    {
        PlayerPrefs.SetInt("FirstScore", 0);
        PlayerPrefs.SetInt("SecondScore", 0);
        PlayerPrefs.SetInt("ThirdScore", 0);
        PlayerPrefs.SetInt("HighScore", 0);
        ShowScoreBoard();
    }
    public void ChangeBackground()
    {
        string newBackgroundName = PlayerPrefs.GetString("CurrentBackground");
        string backgroundPath = "Backgrounds/" + newBackgroundName;
        if (backgroundPath != null)
        {
            _background.GetComponent<Image>().sprite = Resources.Load<Sprite>(backgroundPath);
            Debug.Log("Set Background To: " + newBackgroundName + " Changed Background Successfully!");
        }
        else
        {
            Debug.LogWarning("Not Found Background Path At: " + backgroundPath);
        }
    }
}
