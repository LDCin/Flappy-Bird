using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject _bird;
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _scoreCanvas;
    [SerializeField] private GameObject _tutorialCanvas;
    private Sprite[] birdSprites;
    private Sprite[] backgroundSprites;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        birdSprites = Resources.LoadAll<Sprite>("Birds");
        string birdName = PlayerPrefs.GetString("CurrentBird");
        Sprite currentBird = Resources.Load<Sprite>("Birds/" + birdName);
        Debug.Log("Starting Current Bird Is: " + birdName);

        backgroundSprites = Resources.LoadAll<Sprite>("Backgrounds");
        string backgroundName = PlayerPrefs.GetString("CurrentBackground");
        Sprite currentBackground = Resources.Load<Sprite>("Backgrounds/" + backgroundName);
        Debug.Log("Starting Current Background Is: " + backgroundName);

        if (currentBird != null)
        {
            ChangeBird();
            Debug.Log("Success To Initiate Bird!");
        }
        else
        {
            Debug.Log("Fail To Initiate Bird!");
        }
        if (currentBackground != null)
        {
            ChangeBackground();
            Debug.Log("Success To Initiate Background!");
        }
        else
        {
            Debug.Log("Fail To Initiate Background!");
        }
        _tutorialCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
    public void PlayGame()
    {
        Time.timeScale = 1f;
        ScoreManager.instance.ShowCurrentScore();
        Debug.Log("Play Game!");
    }
    public void HideTutorial()
    {
        _tutorialCanvas.SetActive(false);
        Debug.Log("Hid Tutorial!");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitted Game!");
    }
    public void GameOver()
    {
        _gameOverCanvas.SetActive(true);
        ScoreManager.instance.UpdateScoreBoard();

        ScoreManager.instance.ShowFinalScore();
        Time.timeScale = 0f;
        _bird.GetComponent<Animator>().enabled = false;
        Debug.Log("Game Over!");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
        Debug.Log("Restarted Game!");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuGame");
    }
    public void ChangeBird()
    {
        string newBirdName = PlayerPrefs.GetString("CurrentBird");
        string birdPath = "Birds/" + newBirdName;
        if (birdPath != null)
        {
            _bird.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(birdPath);
            Debug.Log("Set Bird To: " + newBirdName + " And Changed Bird Successfully!");
        }
        else
        {
            Debug.LogWarning("Not Found Bird Path At: " + birdPath);
        }

        string controllerPath = "Animators/" + newBirdName + "/" + newBirdName;
        RuntimeAnimatorController newController = Resources.Load<RuntimeAnimatorController>(controllerPath);
        if (newController != null)
        {
            _bird.GetComponent<Animator>().runtimeAnimatorController = newController;
            Debug.Log("Set Bird Animator To: " + newBirdName + " Controller And Changed Bird Animator Successfully!");
        }
        else
        {
            Debug.LogWarning("Not Found Controller Path At: " + controllerPath);
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
