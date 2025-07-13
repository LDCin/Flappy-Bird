using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomManager : MonoBehaviour
{
    public static CustomManager instance;
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _customCanvas;
    [SerializeField] private GameObject _birdSlotsCanvas;
    [SerializeField] private GameObject _birdSlotPrefab;
    [SerializeField] private GameObject _backgroundSlotsCanvas;
    [SerializeField] private GameObject _backgroundSlotPrefab;
    [SerializeField] private GameObject _birdCustomButton;
    [SerializeField] private GameObject _backgroundCustomButton;
    [SerializeField] private GameObject _selectedSlot;
    [SerializeField] private List<GameObject> _birdList;
    [SerializeField] private List<GameObject> _backgroundList;
    public int index = 0;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        InitiateBirdList();
        InitiateBackgroundList();

        _customCanvas.SetActive(true);
        _birdSlotsCanvas.SetActive(true);
        _backgroundSlotsCanvas.SetActive(false);
        _birdCustomButton.name = "Bird";
        _backgroundCustomButton.name = "Background";

        for (int i = 0; i < _birdList.Count; i++)
        {
            _birdList[i].SetActive(i == 0);
        }
        if (_birdList[0].GetComponent<Image>().name == PlayerPrefs.GetString("CurrentBird")) _selectedSlot.SetActive(true);
        else _selectedSlot.SetActive(false);
        ChangeBackground();
        Debug.Log("Initiate Custom Manager Successfully!");
    }
    public void InitiateBirdList()
    {
        _birdList = new List<GameObject>();
        Sprite[] birdSprites = Resources.LoadAll<Sprite>("Birds");
        for (int i = 0; i < birdSprites.Length; i++)
        {
            GameObject birdSlot = Instantiate(_birdSlotPrefab, _birdSlotsCanvas.transform);

            Image birdImage = birdSlot.GetComponent<Image>();
            if (birdImage != null)
            {
                birdImage.sprite = birdSprites[i];
            }

            birdSlot.name = birdSprites[i].name;

            _birdList.Add(birdSlot);
        }
    }
    public void InitiateBackgroundList()
    {
        _backgroundList = new List<GameObject>();
        Sprite[] backgroundSprites = Resources.LoadAll<Sprite>("Backgrounds");
        for (int i = 0; i < backgroundSprites.Length; i++)
        {
            GameObject backgroundSlot = Instantiate(_backgroundSlotPrefab, _backgroundSlotsCanvas.transform);

            Image backgroundImage = backgroundSlot.GetComponent<Image>();
            if (backgroundImage != null)
            {
                backgroundImage.sprite = backgroundSprites[i];
            }

            backgroundSlot.name = backgroundSprites[i].name;

            _backgroundList.Add(backgroundSlot);
        }
    }
    public void OnCategoryButtonClicked(GameObject button)
    {
        if (button.name == "Bird")
        {
            _birdSlotsCanvas.SetActive(true);
            _backgroundSlotsCanvas.SetActive(false);
            for (int i = 0; i < _birdList.Count; i++)
            {
                _birdList[i].SetActive(i == 0);
            }
            if (_birdList[0].GetComponent<Image>().name == PlayerPrefs.GetString("CurrentBird")) _selectedSlot.SetActive(true);
            else _selectedSlot.SetActive(false);
            Debug.Log("Custom Bird!");
        }
        else if (button.name == "Background")
        {
            _backgroundSlotsCanvas.SetActive(true);
            _birdSlotsCanvas.SetActive(false);
            for (int i = 0; i < _backgroundList.Count; i++)
            {
                _backgroundList[i].SetActive(i == 0);
            }
            Debug.Log("Show Back First Time!");
            index = 0;
            if (_backgroundList[index].GetComponent<Image>().name == PlayerPrefs.GetString("CurrentBackground"))
            {
                _selectedSlot.SetActive(true);
            }
            else _selectedSlot.SetActive(false);
            Debug.Log("Custom Background!");
        }
    }
    public void FindShowingBird()
    {
        for (int i = 0; i < _birdList.Count; i++)
        {
            if (_birdList[i].activeSelf)
            {
                index = i;
                break;
            }
        }
    }
    public void FindShowingBackground()
    {
        for (int i = 0; i < _backgroundList.Count; i++)
        {
            if (_backgroundList[i].activeSelf)
            {
                index = i;
                break;
            }
        }
    }
    public void ShowNextBird()
    {
        FindShowingBird();
        _birdList[index].SetActive(false);
        index = (index + 1) % _birdList.Count;
        _birdList[index].SetActive(true);
    }
    public void ShowPreviousBird()
    {
        FindShowingBird();
        _birdList[index].SetActive(false);
        index = (index - 1 + _birdList.Count) % _birdList.Count;
        _birdList[index].SetActive(true);
    }
    public void ShowNextBackground()
    {
        FindShowingBackground();
        _backgroundList[index].SetActive(false);
        index = (index + 1) % _backgroundList.Count;
        _backgroundList[index].SetActive(true);
    }
    public void ShowPreviousBackground()
    {
        FindShowingBackground();
        _backgroundList[index].SetActive(false);
        index = (index - 1 + _backgroundList.Count) % _backgroundList.Count;
        _backgroundList[index].SetActive(true);
    }
    public void ShowNextSlot()
    {
        if (_birdSlotsCanvas.activeSelf)
        {
            ShowNextBird();
            if (_birdList[index].GetComponent<Image>().name == PlayerPrefs.GetString("CurrentBird"))
            {
                _selectedSlot.SetActive(true);
            }
            else _selectedSlot.SetActive(false);
        }
        else if (_backgroundSlotsCanvas.activeSelf)
        {
            ShowNextBackground();
            if (_backgroundList[index].GetComponent<Image>().name == PlayerPrefs.GetString("CurrentBackground"))
            {
                _selectedSlot.SetActive(true);
            }
            else _selectedSlot.SetActive(false);
        }
    }
    public void ShowPreviosSlot()
    {
        if (_birdSlotsCanvas.activeSelf)
        {
            ShowPreviousBird();
            if (_birdList[index].GetComponent<Image>().name == PlayerPrefs.GetString("CurrentBird"))
            {
                _selectedSlot.SetActive(true);
            }
            else _selectedSlot.SetActive(false);
        }
        else if (_backgroundSlotsCanvas.activeSelf)
        {
            ShowPreviousBackground();
            if (_backgroundList[index].GetComponent<Image>().name == PlayerPrefs.GetString("CurrentBackground"))
            {
                _selectedSlot.SetActive(true);
            }
            else _selectedSlot.SetActive(false);
        }
    }
    public void ApplyChanges()
    {
        if (_birdSlotsCanvas.activeSelf)
        {
            FindShowingBird();
            PlayerPrefs.SetString("CurrentBird", _birdList[index].GetComponent<Image>().sprite.name);
            _selectedSlot.SetActive(true);
        }
        else if (_backgroundSlotsCanvas.activeSelf)
        {
            FindShowingBackground();
            PlayerPrefs.SetString("CurrentBackground", _backgroundList[index].GetComponent<Image>().sprite.name);
            ChangeBackground();
            _selectedSlot.SetActive(true);
        }
    }
    public void Return()
    {
        SceneManager.LoadScene("MenuGame");
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
