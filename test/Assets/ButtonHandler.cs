using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ButtonHandler : MonoBehaviour
{

     
    //Make sure to attach these Buttons in the Inspector
    public Button m_YourFirstButton, proceedButton;

    
    void Start()
    {
        m_YourFirstButton.onClick.AddListener(OnClick);
        proceedButton.onClick.AddListener(ProceedToFight);
    }

    void OnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        manager.RollMachine();
    }

    void ProceedToFight()
    {
        SceneManager.LoadScene("New Scene", LoadSceneMode.Single);
    }


}
