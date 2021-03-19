using System;
using UnityEngine;

public class CoutdownManager : MonoBehaviour
{
    //Attributes
    //References

    //Values
    private bool _start = false;

    [SerializeField]
    private float _startLevelCoutdown = 3f;

    private float _currentstartCoutdown = 0f;

    private bool _startTimerOver = false;

    [SerializeField]
    private float _levelCoutdown = 90;

    private float _currentLevelCoutdown = 0f;

    private bool _timerOver = false;

    //Action - This use for tell to the player if can or not move
    public static Action<bool> ChangePlayerMoveStatus;
    public static Action<int> UpdateStartTimerText;
    public static Action<bool> ShowTimerUI;
    public static Action<float> UpdateTimerText;
    public static Action<float> GetCoutdown;
    public static Action PlayerLoose;
    public static Action StartMusic;

    //Functions
    private void Start()
    {
        Grabber.PlayerWin += PlayerWin;
        GameManager.Pause += Pause;
        if(GetCoutdown != null)
        {
            GetCoutdown(_levelCoutdown);
        }
    }

    //Call by the game manager to indicate that the level can start now
    public void StartCoutdown()
    {
        _start = true;
    }

    void Update()
    {
        if(_start)
        {
            if (!_startTimerOver)
            {
                UpdateStartTimer();
            }

            else if (_startTimerOver && !_timerOver)
            {
                UpdateTimer();
            }
        }
    }

    private void UpdateStartTimer()
    {
        _currentstartCoutdown += Time.deltaTime;

        //Call the action for start coutdown (we don't want to print 0)
        int timerText = Mathf.RoundToInt(3 - _currentstartCoutdown);
        if (UpdateStartTimerText != null && (timerText != 0))
        {
            UpdateStartTimerText(Mathf.RoundToInt(timerText));
        }


        //Check if coutdown is over
        if (_currentstartCoutdown >= _startLevelCoutdown)
        {
            _startTimerOver = true;

            //Call the action for move status
            if(ChangePlayerMoveStatus != null)
            {
                ChangePlayerMoveStatus(true);
            }

            //Call the action for start coutdown
            if(UpdateStartTimerText !=null)
            {
                UpdateStartTimerText(-1);
            }

            //Call the action for show the timer
            if(ShowTimerUI != null)
            {
                ShowTimerUI(true);
            }

            if(StartMusic != null)
            {
                StartMusic();
            }
        }
    }

    private void UpdateTimer()
    {
        //Update the coutdown
        _currentLevelCoutdown += Time.deltaTime;

        //Call the action for update the time on the UI
        if(UpdateTimerText != null)
        {
            UpdateTimerText(_levelCoutdown - _currentLevelCoutdown);
        }

        //Timer is over
        if(_currentLevelCoutdown >= _levelCoutdown)
        {
            _timerOver = true;

            //Call the action for stop player
            if (ChangePlayerMoveStatus != null)
            {
                ChangePlayerMoveStatus(false);
            }

            //Call the action for stop timer
            if(ShowTimerUI != null)
            {
                ShowTimerUI(false);
            }

            //Call the action for show the end menu
            if(PlayerLoose != null)
            {
                PlayerLoose();
            }
        }
    }

    //Player win
    private void PlayerWin()
    {
        _start = false;
    }

    private void Pause(bool b)
    {
        _start = !b;
    }
}
