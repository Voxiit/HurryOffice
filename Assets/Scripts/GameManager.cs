﻿using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Attributes
    //References
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private InitializeLevel _initializeLevel;

    [SerializeField]
    private CoutdownManager _coutdownManager;

    //Values
    //Action - This use for tell to the player if can or not move
    public static Action<bool> ShowResultUI;
    public static Action<bool> ShowExplanation1;
    public static Action<bool> ShowExplanation2;
    public static Action StopMusic;

    private bool _explanation1 = false;
    private bool _explanation2 = false;

    //Functions
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        _initializeLevel.Initialize();
        ShowExplanation1(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && (!_explanation1 || !_explanation2))
        {
            if(!_explanation1)
            {
                _explanation1 = true;
                ShowExplanation1(false);
                ShowExplanation2(true);
            }
            else if(!_explanation2)
            {
                _explanation2 = true;
                ShowExplanation2(false);
                _coutdownManager.StartCoutdown();
            }

        }

    }

    //The two following functions are made for the buttons
    public void Restart()
    {
        //Action for stop the musique
        if (StopMusic != null)
        {
            StopMusic();
        }

        SceneManager.LoadScene("MainLevel");

        //Action for hide the UI
        if(ShowResultUI != null)
        {
            ShowResultUI(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}