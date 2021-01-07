using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    public string startGame;
    private AudioSource MainSong;

    public void Start()
    {
        MainSong = GameObject.Find("MainSong").GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(startGame);
        MainSong.Stop();
    }

    public void QuitGame()
    {
        Application.Quit();
        MainSong.Stop();
    }
}
