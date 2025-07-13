using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    [SerializeField] private GameObject _background;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (!PlayerPrefs.HasKey("CurrentBird"))
        {
            PlayerPrefs.SetString("CurrentBird", "BlueBird");
        }
        if (!PlayerPrefs.HasKey("CurrentBackground"))
        {
            PlayerPrefs.SetString("CurrentBackground", "DayBackground");
        }
        ChangeBackground();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
        Debug.Log("Start Game!");
    }
    public void Custom()
    {
        SceneManager.LoadScene("Custom");
        Debug.Log("Custom!");
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        GameManager.instance.QuitGame();
    }
    public void Rank()
    {
        Debug.Log("Rank!");
        SceneManager.LoadScene("Rank");
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
