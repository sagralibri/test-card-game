using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public Button startButton;
    public Campaign heist;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton.onClick.AddListener(MenuClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MenuClick()
    {
        manager.SetCampaign(heist);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
