using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    //Attributes
    //References
    [SerializeField]
    private Canvas _mainCavas;

    [SerializeField]
    private Image _startImage;

    [SerializeField]
    private Canvas _explanation1;

    [SerializeField]
    private Canvas _explanation2;

    [SerializeField]
    private TextMeshProUGUI _startText;

    [SerializeField]
    private Image _timerImage;

    [SerializeField]
    private TextMeshProUGUI _timerText;

    [SerializeField]
    private Image _resultImage;

    [SerializeField]
    private Canvas _restartQuitCanvas;

    [SerializeField]
    private Canvas _winCanvas;

    [SerializeField]
    private Canvas _looseCanvas;

    //Values

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        CoutdownManager.UpdateStartTimerText += UpdateStartText;
        CoutdownManager.ShowTimerUI += ShowTimerUI;
        CoutdownManager.UpdateTimerText += UpdateTimer;
        CoutdownManager.PlayerLoose += PlayerLooseUI;
        GameManager.ShowResultUI += ShowResultUI;
        GameManager.ShowExplanation1 += ShowExplanation1;
        GameManager.ShowExplanation2 += ShowExplanation2;
        Grabber.PlayerWin += PlayerWin;
        ShowStartUI(true);
    }


    //Start funciton
    private void UpdateStartText(int time)
    {
        if(time == -1)
        {
            _startText.text = "GO!";
            ShowStartUI(false);
        }
        else
        {
            _startText.text = time.ToString();
        }
    }

    private void ShowStartUI(bool show)
    {
        _startImage.gameObject.SetActive(show);
        _startText.gameObject.SetActive(show);
    }

    //Timer function
    private void UpdateTimer(float currentTime)
    {
        //Get seconds and milliseconds
        int seconds = Mathf.RoundToInt(currentTime);
        double miliseconds = currentTime % 1;
        miliseconds = Math.Round(miliseconds, 2);
        miliseconds *= 100;

        //Update the text
        _timerText.text = seconds < 10 ? "0" + seconds + " :" + miliseconds : seconds + " :" + miliseconds;
    }

    private void ShowTimerUI(bool show)
    {
        _timerImage.gameObject.SetActive(show);
        _timerText.gameObject.SetActive(show);
    }

    //Result function
    private void PlayerWinUi()
    {
        ShowResultUI(true);
        _looseCanvas.gameObject.SetActive(false);
        _winCanvas.gameObject.SetActive(true);
    }

    private void PlayerLooseUI()
    {
        ShowResultUI(true);
        _looseCanvas.gameObject.SetActive(true);
        _winCanvas.gameObject.SetActive(false);
    }

    private void ShowResultUI(bool show)
    {
        //We unlock the mouse
        Cursor.lockState = CursorLockMode.Confined;

        _resultImage.gameObject.SetActive(show);
        _restartQuitCanvas.gameObject.SetActive(show);
    }

    //Player win
    private void PlayerWin()
    {
        ShowTimerUI(false);
        PlayerWinUi();
    }

    //Explanation Canvas
    private void ShowExplanation1(bool show)
    {
        _explanation1.gameObject.SetActive(show);
    }

    private void ShowExplanation2(bool show)
    {
        _explanation2.gameObject.SetActive(show);
    }

}
